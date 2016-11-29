//	File:		dlgAction.cs
//	Author:	    jheary
//	Date:		01/07/09
//	Desc:		Dialog to create a new action for an issue.
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Argix.Enterprise;
using Word=Microsoft.Office.Interop.Word;

namespace Argix.CustomerSvc {
    //
    public partial class dlgAction :Form {
        //Members
        private Issue.Action mAction = null;
        private ToolTip mToolTip = null;
        private const string CMD_CANCEL = "&Cancel";
        private const string CMD_OK = "O&K";
        private const string CMD_SPELLCHECK = "&Spell Check";

        //Interface
        public dlgAction(Issue.Action action) {
            //Constructor
            try {
                InitializeComponent();
                this.btnOk.Text = CMD_OK;
                this.btnCancel.Text = CMD_CANCEL;
                this.btnSpellCheck.Text = CMD_SPELLCHECK;
                this.mAction = action;
                this.Text = "Action (New) for " + this.mAction.Parent.Subject;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgAction instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mToolTip = new ToolTip();
                this.mToolTip.ShowAlways = true;
                this.mToolTip.SetToolTip(this.cboActionType,"Select an action type.");

                this.mActionsDS.Merge(this.mAction.GetValidActionTypes());
                this.cboActionType.SelectedIndex = -1;
                this.txtComment.Text = this.mAction.Comment;
                this.txtComments.Text = this.mAction.Parent.GetAllActionComments();
                this.chkRunning.Checked = true;
                OnRunningChecked(null,EventArgs.Empty);
                for(int i=0;i<this.mAction.Attachments.AttachmentTable.Rows.Count;i++) {
                    System.IO.FileInfo fi = new System.IO.FileInfo(this.mAction.Attachments.AttachmentTable[i].FileName);
                    this.lsvAttachments.Items.Add(fi.Name);
                }
                OnValidateForm(null,EventArgs.Empty);
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            try {
                this.btnOk.Enabled = false;
                this.btnSpellCheck.Enabled = this.txtComment.Text.Length > 0;
                this.lblSpellCheck.Text = "";
                this.btnOk.Enabled = (this.cboActionType.Text.Length > 0 && this.txtComment.Text.Length > 0);
            }
            catch { }
        }
        private void OnActionTypeSelected(object sender,EventArgs e) {
            //Event handler for change in selected action type
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnRunningChecked(object sender,EventArgs e) {
            //Event handler for running checked
            this.txtComments.Visible = this.chkRunning.Checked;
        }
        private void OnCmdClick(object sender,System.EventArgs e) {
            //Command button handler
            try {
                Button btn = (Button)sender;
                switch(btn.Text) {
                    case CMD_CANCEL:
                        //Close the dialog
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    case CMD_OK:
                        //Initialize new action
                        this.Cursor = Cursors.WaitCursor;
                        this.DialogResult = DialogResult.OK;
                        this.mAction.TypeID = (byte)this.cboActionType.SelectedValue;
                        this.mAction.Comment = this.txtComment.Text;
                        for(int i=0;i<this.lsvAttachments.Items.Count;i++) {
                            //Remove unselected attachments from the action
                            ListViewItem item = this.lsvAttachments.Items[i];
                            if(!item.Checked) this.mAction.Remove(item.Text);
                        }
                        this.Close();
                        break;
                    case CMD_SPELLCHECK:
                        spellCheck(this.txtComment,this.lblSpellCheck);
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        #region Local Services: spellCheck(), reportError()
        private void spellCheck(TextBox txt,Label lbl) {
            //Spell check
            if(txt.Text.Length > 0) {
                //Create an instance of MS Word application
                Word.Application app = new Word.Application();
                app.Visible = false;

                //Setup spell checker with text to check and count errors
                //Setting these variables is comparable to passing null to the function
                //This is necessary because the C# null cannot be passed by reference
                object temp=Missing.Value,newTemp=Missing.Value,docType=Missing.Value,vis=true;
                Word.Document doc = app.Documents.Add(ref temp,ref newTemp,ref docType,ref vis);
                doc.Words.First.InsertBefore(txt.Text);
                int errCount = doc.SpellingErrors.Count;

                //Run the checker
                object opt = Missing.Value;
                doc.CheckSpelling(ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt);

                //Update original text
                object first = 0,last = doc.Characters.Count - 1;
                txt.Text = doc.Range(ref first,ref last).Text;
                txt.Text = txt.Text.Replace("\r","\r\n");
                lbl.Text = "Spelling OK. " + errCount + " error(s) corrected ";

                //Close MS Word application
                object save = false,format = Missing.Value,rtDoc = Missing.Value;
                app.Quit(ref save,ref format,ref rtDoc);
            }
        }
        private void reportError(Exception ex) { MessageBox.Show("Unexpected error:\n\n" + ex.ToString()); }
        #endregion
    }
}