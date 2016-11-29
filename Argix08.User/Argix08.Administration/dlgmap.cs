//	File:	dlgmap.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to create\copy a new or view\edit an existing Delivery Map.
//			mMapViewDS- view for mappings (map header excluded) 
//			mMapDS- map detail (header, mappings) for transactions to middle tier
//			mMappingsDS- bound to grdOverrides for edit of new\update\delete ;
//						  mappings; includes display of prior path\service values
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Tsort.Enterprise;

namespace Tsort {
	//
	public enum MapTypeEnum { MapTypeTerminal = 1, MapTypeClient = 2 }
	public enum MapActionEnum { MapActionView = 1, MapActionCreate = 2, MapActionCopy = 3, MapActionEdit = 4 }
	public class dlgMapDetail : System.Windows.Forms.Form {
		//Members
		private MapTypeEnum mType=MapTypeEnum.MapTypeTerminal;
		private MapActionEnum mAction=MapActionEnum.MapActionView;
		private bool mIsDragging=false;
		#region Controls

		private Tsort.Enterprise.MapDS mMapDS;
		private Tsort.Windows.SelectionList mTerminalsDS;
		private Tsort.Windows.SelectionList mClientsDS;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.ComboBox cboSortCenter;
		private System.Windows.Forms.Label _lblDesc;
		private System.Windows.Forms.Label _lblClient;
		private System.Windows.Forms.Label _lblSortCenter;
		private System.Windows.Forms.TextBox txtDesc;
		private System.Windows.Forms.ComboBox cboClient;
		private System.Windows.Forms.GroupBox fraMap;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnEdit;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMapView;
		private System.Windows.Forms.Button btnDel;
		private Tsort.Enterprise.MapDS mMappingsDS;
		private Tsort.Windows.SelectionList mTsortPathsDS;
		private Tsort.Windows.SelectionList mReturnPathsDS;
		private Tsort.Windows.SelectionList mTsortServicesDS;
		private Tsort.Windows.SelectionList mReturnServicesDS;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.GroupBox fraOverrides;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboCountry;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdOverride;
		private System.Windows.Forms.Button btnApply;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboTsortPath;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboReturnService;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboReturnPath;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboTsortService;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRem;
		private System.Windows.Forms.Splitter splitterH;
		private Tsort.Enterprise.StateDS mStatesDS;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cboState;
		private Tsort.Enterprise.MapDS mMapViewDS;
		private System.Windows.Forms.ContextMenu ctxMap;
		private System.Windows.Forms.MenuItem ctxMapEdit;
		private System.Windows.Forms.MenuItem ctxMapDelete;
		private Tsort.Enterprise.MapDS m_dsMappingView;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_EDIT = "&Edit";
		private const string CMD_DEL = "&Delete";
		private const string CMD_ADD = "&Add";
		private const string CMD_REM = "&Remove";
		private const string CMD_APPLY = "&Apply";
		private const string CMD_CLOSE = "&Close";
		private const string MNU_EDIT = "&Edit MapDS";
		private const string MNU_REMOVE = "&Remove MapDS";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgMapDetail(string mapID, int sortCenterID, MapTypeEnum mapType, MapActionEnum mapAction) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();				
				this.btnEdit.Text = CMD_EDIT;
				this.btnDel.Text = CMD_DEL;
				this.btnAdd.Text = CMD_ADD;
				this.btnRem.Text = CMD_REM;
				this.btnApply.Text = CMD_APPLY;
				this.btnClose.Text = CMD_CLOSE;
				this.ctxMapEdit.Text = MNU_EDIT;
				this.ctxMapDelete.Text = MNU_REMOVE;
				#region Split window
				this.splitterH.MinExtra = 24;
				this.splitterH.MinSize = 48;
				this.fraMap.Dock = DockStyle.Fill;
				this.splitterH.Dock = DockStyle.Bottom;
				this.fraOverrides.Dock = DockStyle.Bottom;
				this.pnlMain.Controls.AddRange(new Control[]{this.splitterH, this.fraMap, this.fraOverrides});
				#endregion
				
				//Set mediator service, view data, and titlebar caption
				this.mType = mapType;
				this.mAction = mapAction;

				//Get a map view (header\mappings)
				setViewAndDetail(mapID, sortCenterID);
			} 
			catch(Exception ex) { throw ex; }
		}
		private void setViewAndDetail(string mapID, int sortCenterID) {
			//Event handler for mapping cell activated
			MapDS.PostalCodeMappingTableRow rowMapping;
			MapDS.PostalCodeMappingTableRow _rowMapping;
			string sSortCenter="", sClient="";
			try {
				this.mMapViewDS.Merge(EnterpriseFactory.GetMap(mapID, sortCenterID));
				if(this.mMapViewDS.MapDetailTable.Count > 0) {
					//Set the detail data
					this.mMapDS.Clear();
					this.mMapDS.Merge(this.mMapViewDS);
					this.mMapDS.PostalCodeMappingTable.Clear();
					switch(this.mAction) {
						case MapActionEnum.MapActionCreate:	
							//New header, no new mappings
							this.Text = (this.mType==MapTypeEnum.MapTypeClient) ? "Client Map (New)" : "Terminal Map (New)"; 
							break;
						case MapActionEnum.MapActionCopy:	
							//Clear the header, copy the new mappings, and clear the original mappings from the view
							this.mMapDS.MapDetailTable[0].MapID = "";
							this.mMapDS.MapDetailTable[0].SetSortCenterIDNull();
							this.mMapDS.MapDetailTable[0].SetClientIDNull();
							this.mMapDS.MapDetailTable[0].Description = "";
							this.mMapDS.MapDetailTable[0].IsActive = true;
							this.mMappingsDS.Merge(this.mMapViewDS);
							this.mMapViewDS.PostalCodeMappingTable.Clear();
							for(int i=0; i<this.mMappingsDS.PostalCodeMappingTable.Rows.Count; i++) {
								_rowMapping = this.mMappingsDS.PostalCodeMappingTable[i];
								_rowMapping.MapID = "";
							//if(!_rowMapping.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = _rowMapping.PathIDTsort;
							//if(!_rowMapping.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = _rowMapping.TsortPathMnemonic;
							//if(!_rowMapping.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = _rowMapping.ServiceIDTsort;
							//if(!_rowMapping.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = _rowMapping.TsortServiceMnemonic;
							//if(!_rowMapping.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = _rowMapping.PathIDReturns;
							//if(!_rowMapping.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = _rowMapping.ReturnPathMnemonic;
							//if(!_rowMapping.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = _rowMapping.ServiceIDReturns;
							//if(!_rowMapping.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = _rowMapping.ReturnServiceMnemonic;
							}
							sSortCenter = (!this.mMapViewDS.MapDetailTable[0].IsSortCenterNull()) ? this.mMapViewDS.MapDetailTable[0].SortCenter : "";
							sClient = (!this.mMapViewDS.MapDetailTable[0].IsClientNameNull()) ? this.mMapViewDS.MapDetailTable[0].ClientName : "";
							this.Text = (this.mType==MapTypeEnum.MapTypeClient) ? "Client Map (New- copy of " + sClient + " for " + sSortCenter + ")" : "Terminal Map (New- copy of " + sSortCenter + ")"; 
							break;
						case MapActionEnum.MapActionEdit:	
							//Copy the header, view existing mappings, no new mappings
							this.m_dsMappingView.Clear();
							for(int i=0; i<this.mMapViewDS.PostalCodeMappingTable.Rows.Count; i++) {
								//Use heirarchical grid for terminal maps only; put non-USA in as 3 position
								rowMapping = this.mMapViewDS.PostalCodeMappingTable[i];
								if(this.mType==MapTypeEnum.MapTypeClient || (rowMapping.Country=="USA" && rowMapping.PostalCode.Length==3) || rowMapping.Country!="USA")
									this.m_dsMappingView.PostalCodeMappingTable3.ImportRow(rowMapping);
								else {
									this.m_dsMappingView.PostalCodeMappingTable5.ImportRow(rowMapping);
									this.m_dsMappingView.PostalCodeMappingTable5[this.m_dsMappingView.PostalCodeMappingTable5.Rows.Count-1].PostalCodeID = rowMapping.PostalCode.Substring(0, 3);
								}
							}
							this.Text = (this.mType==MapTypeEnum.MapTypeClient) ? "Client Map (" + mapID.Trim() + ")" : "Terminal Map (" + mapID.Trim() + ")"; 
							break;
						case MapActionEnum.MapActionView:	
							//Copy the header, view existing mappings
							for(int i=0; i<this.mMapViewDS.PostalCodeMappingTable.Rows.Count; i++) {
								//Use heirarchical grid for terminal maps only; put non-USA in as 3 position
								rowMapping = this.mMapViewDS.PostalCodeMappingTable[i];
								if(this.mType==MapTypeEnum.MapTypeClient || (rowMapping.Country=="USA" && rowMapping.PostalCode.Length==3) || rowMapping.Country!="USA")
									this.m_dsMappingView.PostalCodeMappingTable3.ImportRow(rowMapping);
								else {
									this.m_dsMappingView.PostalCodeMappingTable5.ImportRow(rowMapping);
									this.m_dsMappingView.PostalCodeMappingTable5[this.m_dsMappingView.PostalCodeMappingTable5.Rows.Count-1].PostalCodeID = rowMapping.PostalCode.Substring(0, 3);
								}
							}
							this.Text = (this.mType==MapTypeEnum.MapTypeClient) ? "Client Map (" + mapID.Trim() + ")" : "Terminal Map (" + mapID.Trim() + ")"; 
							break;
						default:							
							this.Text = (this.mType==MapTypeEnum.MapTypeClient) ? "Client Map (Action Unknown)" : "Terminal Map (Action Unknown)"; 
							break;
					}
				}
				else
					this.Text = "Map (Data Unavailable)";
			} 
			catch(Exception ex) { reportError(ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PostalCodeMappingTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MapID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CountryID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Country");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldPathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldTsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldTsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldTsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldPathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OldReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowAction");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PostalCodeMappingTable3", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MapID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CountryID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Country");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCodeMappingTable3PostalCodeMappingTable5");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PostalCodeMappingTable3PostalCodeMappingTable5", 0);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MapID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CountryID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Country");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDTsort");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TsortServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PathIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnPathLastStopMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceIDReturns");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReturnServiceMnemonic");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn79 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn80 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn81 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCodeID");
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			this.mClientsDS = new Tsort.Windows.SelectionList();
			this.mTerminalsDS = new Tsort.Windows.SelectionList();
			this.mMapDS = new Tsort.Enterprise.MapDS();
			this.mMappingsDS = new Tsort.Enterprise.MapDS();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.splitterH = new System.Windows.Forms.Splitter();
			this.fraOverrides = new System.Windows.Forms.GroupBox();
			this.btnRem = new System.Windows.Forms.Button();
			this.grdOverride = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.cboTsortPath = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboReturnService = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboReturnPath = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboTsortService = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.btnAdd = new System.Windows.Forms.Button();
			this.cboCountry = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboState = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.fraMap = new System.Windows.Forms.GroupBox();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnEdit = new System.Windows.Forms.Button();
			this.grdMapView = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxMap = new System.Windows.Forms.ContextMenu();
			this.ctxMapEdit = new System.Windows.Forms.MenuItem();
			this.ctxMapDelete = new System.Windows.Forms.MenuItem();
			this.m_dsMappingView = new Tsort.Enterprise.MapDS();
			this.mMapViewDS = new Tsort.Enterprise.MapDS();
			this.btnClose = new System.Windows.Forms.Button();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.cboSortCenter = new System.Windows.Forms.ComboBox();
			this._lblDesc = new System.Windows.Forms.Label();
			this._lblClient = new System.Windows.Forms.Label();
			this._lblSortCenter = new System.Windows.Forms.Label();
			this.txtDesc = new System.Windows.Forms.TextBox();
			this.cboClient = new System.Windows.Forms.ComboBox();
			this.btnApply = new System.Windows.Forms.Button();
			this.mTsortPathsDS = new Tsort.Windows.SelectionList();
			this.mReturnPathsDS = new Tsort.Windows.SelectionList();
			this.mTsortServicesDS = new Tsort.Windows.SelectionList();
			this.mReturnServicesDS = new Tsort.Windows.SelectionList();
			this.mCountriesDS = new Tsort.Enterprise.CountryDS();
			this.mStatesDS = new Tsort.Enterprise.StateDS();
			((System.ComponentModel.ISupportInitialize)(this.mClientsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTerminalsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mMapDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mMappingsDS)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.fraOverrides.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdOverride)).BeginInit();
			this.fraMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdMapView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dsMappingView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mMapViewDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortPathsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnPathsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortServicesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnServicesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).BeginInit();
			this.SuspendLayout();
			// 
			// mClientsDS
			// 
			this.mClientsDS.DataSetName = "SelectionList";
			this.mClientsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mTerminalsDS
			// 
			this.mTerminalsDS.DataSetName = "SelectionList";
			this.mTerminalsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mMapDS
			// 
			this.mMapDS.DataSetName = "MapDS";
			this.mMapDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mMappingsDS
			// 
			this.mMappingsDS.DataSetName = "MapDS";
			this.mMappingsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// pnlMain
			// 
			this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMain.Controls.Add(this.splitterH);
			this.pnlMain.Controls.Add(this.fraOverrides);
			this.pnlMain.Controls.Add(this.fraMap);
			this.pnlMain.DockPadding.All = 3;
			this.pnlMain.Location = new System.Drawing.Point(0, 63);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(858, 360);
			this.pnlMain.TabIndex = 20;
			// 
			// splitterH
			// 
			this.splitterH.BackColor = System.Drawing.SystemColors.ControlText;
			this.splitterH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterH.Location = new System.Drawing.Point(3, 186);
			this.splitterH.MinExtra = 12;
			this.splitterH.MinSize = 12;
			this.splitterH.Name = "splitterH";
			this.splitterH.Size = new System.Drawing.Size(852, 3);
			this.splitterH.TabIndex = 26;
			this.splitterH.TabStop = false;
			// 
			// fraOverrides
			// 
			this.fraOverrides.Controls.Add(this.btnRem);
			this.fraOverrides.Controls.Add(this.grdOverride);
			this.fraOverrides.Controls.Add(this.cboTsortPath);
			this.fraOverrides.Controls.Add(this.cboReturnService);
			this.fraOverrides.Controls.Add(this.cboReturnPath);
			this.fraOverrides.Controls.Add(this.cboTsortService);
			this.fraOverrides.Controls.Add(this.btnAdd);
			this.fraOverrides.Controls.Add(this.cboCountry);
			this.fraOverrides.Controls.Add(this.cboState);
			this.fraOverrides.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.fraOverrides.Location = new System.Drawing.Point(3, 189);
			this.fraOverrides.Name = "fraOverrides";
			this.fraOverrides.Size = new System.Drawing.Size(852, 168);
			this.fraOverrides.TabIndex = 25;
			this.fraOverrides.TabStop = false;
			this.fraOverrides.Text = "Updates\\DeliveryLocationDS";
			// 
			// btnRem
			// 
			this.btnRem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRem.BackColor = System.Drawing.SystemColors.Control;
			this.btnRem.Enabled = false;
			this.btnRem.Location = new System.Drawing.Point(750, 138);
			this.btnRem.Name = "btnRem";
			this.btnRem.Size = new System.Drawing.Size(96, 24);
			this.btnRem.TabIndex = 9;
			this.btnRem.Text = "&Remove";
			this.btnRem.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// grdOverride
			// 
			this.grdOverride.AllowDrop = true;
			this.grdOverride.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdOverride.DataMember = "PostalCodeMappingTable";
			this.grdOverride.DataSource = this.mMappingsDS;
			this.grdOverride.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdOverride.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Hidden = true;
			ultraGridColumn2.Hidden = true;
			ultraGridColumn3.Header.VisiblePosition = 4;
			ultraGridColumn3.Hidden = true;
			ultraGridColumn4.Header.VisiblePosition = 22;
			ultraGridColumn4.Hidden = true;
			ultraGridColumn5.Header.VisiblePosition = 17;
			ultraGridColumn5.Hidden = true;
			ultraGridColumn6.Header.VisiblePosition = 27;
			ultraGridColumn6.Hidden = true;
			ultraGridColumn7.Header.VisiblePosition = 16;
			ultraGridColumn7.Hidden = true;
			ultraGridColumn8.Header.VisiblePosition = 10;
			ultraGridColumn8.Hidden = true;
			ultraGridColumn9.Header.Caption = "Country";
			ultraGridColumn9.Header.VisiblePosition = 3;
			ultraGridColumn9.Width = 60;
			ultraGridColumn10.Header.VisiblePosition = 6;
			ultraGridColumn10.Hidden = true;
			ultraGridColumn11.Header.VisiblePosition = 2;
			ultraGridColumn11.Width = 93;
			ultraGridColumn12.Header.Caption = "State";
			ultraGridColumn12.Header.VisiblePosition = 8;
			ultraGridColumn12.Width = 43;
			ultraGridColumn13.Header.VisiblePosition = 7;
			ultraGridColumn13.Hidden = true;
			ultraGridColumn14.Header.Caption = "TsortPath";
			ultraGridColumn14.Header.VisiblePosition = 9;
			ultraGridColumn14.Width = 75;
			ultraGridColumn15.Header.VisiblePosition = 12;
			ultraGridColumn15.Hidden = true;
			ultraGridColumn16.Header.VisiblePosition = 5;
			ultraGridColumn16.Hidden = true;
			ultraGridColumn17.Header.Caption = "TsortSvc";
			ultraGridColumn17.Header.VisiblePosition = 14;
			ultraGridColumn17.Width = 75;
			ultraGridColumn18.Header.VisiblePosition = 28;
			ultraGridColumn18.Hidden = true;
			ultraGridColumn19.Header.Caption = "RetrnPath";
			ultraGridColumn19.Header.VisiblePosition = 20;
			ultraGridColumn19.Width = 75;
			ultraGridColumn20.Header.VisiblePosition = 29;
			ultraGridColumn20.Hidden = true;
			ultraGridColumn21.Header.VisiblePosition = 13;
			ultraGridColumn21.Hidden = true;
			ultraGridColumn22.Header.Caption = "RetrnSvc";
			ultraGridColumn22.Header.VisiblePosition = 25;
			ultraGridColumn22.Width = 75;
			ultraGridColumn23.Header.Caption = "_TsortPath";
			ultraGridColumn23.Header.VisiblePosition = 11;
			ultraGridColumn23.Width = 75;
			ultraGridColumn24.Header.VisiblePosition = 18;
			ultraGridColumn24.Hidden = true;
			ultraGridColumn25.Header.VisiblePosition = 19;
			ultraGridColumn25.Hidden = true;
			ultraGridColumn26.Header.Caption = "_TsortSvc";
			ultraGridColumn26.Header.VisiblePosition = 15;
			ultraGridColumn26.Width = 75;
			ultraGridColumn27.Header.VisiblePosition = 30;
			ultraGridColumn27.Hidden = true;
			ultraGridColumn28.Header.Caption = "_RetrnPath";
			ultraGridColumn28.Header.VisiblePosition = 21;
			ultraGridColumn28.Width = 75;
			ultraGridColumn29.Header.VisiblePosition = 31;
			ultraGridColumn29.Hidden = true;
			ultraGridColumn30.Header.VisiblePosition = 24;
			ultraGridColumn30.Hidden = true;
			ultraGridColumn31.Header.Caption = "_RetrnSvc";
			ultraGridColumn31.Header.VisiblePosition = 26;
			ultraGridColumn31.Width = 75;
			ultraGridColumn32.Header.VisiblePosition = 23;
			ultraGridColumn32.Hidden = true;
			ultraGridColumn33.Header.VisiblePosition = 36;
			ultraGridColumn33.Hidden = true;
			ultraGridColumn34.Header.VisiblePosition = 35;
			ultraGridColumn34.Hidden = true;
			ultraGridColumn35.Hidden = true;
			ultraGridColumn36.Header.VisiblePosition = 33;
			ultraGridColumn36.Width = 60;
			ultraGridColumn37.Header.VisiblePosition = 32;
			ultraGridColumn37.Hidden = true;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			ultraGridBand1.Columns.Add(ultraGridColumn5);
			ultraGridBand1.Columns.Add(ultraGridColumn6);
			ultraGridBand1.Columns.Add(ultraGridColumn7);
			ultraGridBand1.Columns.Add(ultraGridColumn8);
			ultraGridBand1.Columns.Add(ultraGridColumn9);
			ultraGridBand1.Columns.Add(ultraGridColumn10);
			ultraGridBand1.Columns.Add(ultraGridColumn11);
			ultraGridBand1.Columns.Add(ultraGridColumn12);
			ultraGridBand1.Columns.Add(ultraGridColumn13);
			ultraGridBand1.Columns.Add(ultraGridColumn14);
			ultraGridBand1.Columns.Add(ultraGridColumn15);
			ultraGridBand1.Columns.Add(ultraGridColumn16);
			ultraGridBand1.Columns.Add(ultraGridColumn17);
			ultraGridBand1.Columns.Add(ultraGridColumn18);
			ultraGridBand1.Columns.Add(ultraGridColumn19);
			ultraGridBand1.Columns.Add(ultraGridColumn20);
			ultraGridBand1.Columns.Add(ultraGridColumn21);
			ultraGridBand1.Columns.Add(ultraGridColumn22);
			ultraGridBand1.Columns.Add(ultraGridColumn23);
			ultraGridBand1.Columns.Add(ultraGridColumn24);
			ultraGridBand1.Columns.Add(ultraGridColumn25);
			ultraGridBand1.Columns.Add(ultraGridColumn26);
			ultraGridBand1.Columns.Add(ultraGridColumn27);
			ultraGridBand1.Columns.Add(ultraGridColumn28);
			ultraGridBand1.Columns.Add(ultraGridColumn29);
			ultraGridBand1.Columns.Add(ultraGridColumn30);
			ultraGridBand1.Columns.Add(ultraGridColumn31);
			ultraGridBand1.Columns.Add(ultraGridColumn32);
			ultraGridBand1.Columns.Add(ultraGridColumn33);
			ultraGridBand1.Columns.Add(ultraGridColumn34);
			ultraGridBand1.Columns.Add(ultraGridColumn35);
			ultraGridBand1.Columns.Add(ultraGridColumn36);
			ultraGridBand1.Columns.Add(ultraGridColumn37);
			appearance2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.ActiveRowAppearance = appearance2;
			appearance3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance3.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance3.FontData.Name = "Verdana";
			appearance3.FontData.SizeInPoints = 8F;
			appearance3.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.HeaderAppearance = appearance3;
			appearance4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance4.FontData.Name = "Verdana";
			appearance4.FontData.SizeInPoints = 8F;
			appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.RowAlternateAppearance = appearance4;
			appearance5.BackColor = System.Drawing.Color.White;
			appearance5.FontData.Name = "Verdana";
			appearance5.FontData.SizeInPoints = 8F;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance5.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.RowAppearance = appearance5;
			this.grdOverride.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdOverride.DisplayLayout.CaptionAppearance = appearance6;
			this.grdOverride.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdOverride.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdOverride.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdOverride.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdOverride.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdOverride.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdOverride.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdOverride.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdOverride.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdOverride.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdOverride.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdOverride.Location = new System.Drawing.Point(6, 25);
			this.grdOverride.Name = "grdOverride";
			this.grdOverride.Size = new System.Drawing.Size(840, 107);
			this.grdOverride.TabIndex = 3;
			this.grdOverride.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
			this.grdOverride.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnMappingSelectionChanged);
			this.grdOverride.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnMappingRowUpdated);
			this.grdOverride.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnMappingCellChanged);
			this.grdOverride.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnMappingDragDrop);
			this.grdOverride.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnMappingDragEnter);
			this.grdOverride.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnBeforeMappingCellActivated);
			this.grdOverride.DragOver += new System.Windows.Forms.DragEventHandler(this.OnMappingDragOver);
			// 
			// cboTsortPath
			// 
			this.cboTsortPath.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboTsortPath.Location = new System.Drawing.Point(264, 15);
			this.cboTsortPath.Name = "cboTsortPath";
			this.cboTsortPath.Size = new System.Drawing.Size(69, 22);
			this.cboTsortPath.TabIndex = 4;
			this.cboTsortPath.Text = null;
			this.cboTsortPath.Visible = false;
			// 
			// cboReturnService
			// 
			this.cboReturnService.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboReturnService.Location = new System.Drawing.Point(705, 15);
			this.cboReturnService.Name = "cboReturnService";
			this.cboReturnService.Size = new System.Drawing.Size(69, 22);
			this.cboReturnService.TabIndex = 7;
			this.cboReturnService.Text = null;
			this.cboReturnService.Visible = false;
			// 
			// cboReturnPath
			// 
			this.cboReturnPath.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboReturnPath.Location = new System.Drawing.Point(558, 15);
			this.cboReturnPath.Name = "cboReturnPath";
			this.cboReturnPath.Size = new System.Drawing.Size(69, 22);
			this.cboReturnPath.TabIndex = 6;
			this.cboReturnPath.Text = null;
			this.cboReturnPath.Visible = false;
			// 
			// cboTsortService
			// 
			this.cboTsortService.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboTsortService.Location = new System.Drawing.Point(411, 15);
			this.cboTsortService.Name = "cboTsortService";
			this.cboTsortService.Size = new System.Drawing.Size(69, 22);
			this.cboTsortService.TabIndex = 5;
			this.cboTsortService.Text = null;
			this.cboTsortService.Visible = false;
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAdd.BackColor = System.Drawing.SystemColors.Control;
			this.btnAdd.Enabled = false;
			this.btnAdd.Location = new System.Drawing.Point(648, 138);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(96, 24);
			this.btnAdd.TabIndex = 3;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// cboCountry
			// 
			this.cboCountry.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCountry.Location = new System.Drawing.Point(87, 18);
			this.cboCountry.MaxDropDownItems = 5;
			this.cboCountry.MaxLength = 99999;
			this.cboCountry.Name = "cboCountry";
			this.cboCountry.Size = new System.Drawing.Size(54, 22);
			this.cboCountry.TabIndex = 8;
			this.cboCountry.Text = null;
			this.cboCountry.Visible = false;
			// 
			// cboState
			// 
			this.cboState.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboState.Location = new System.Drawing.Point(147, 18);
			this.cboState.MaxDropDownItems = 5;
			this.cboState.MaxLength = 99999;
			this.cboState.Name = "cboState";
			this.cboState.Size = new System.Drawing.Size(45, 22);
			this.cboState.TabIndex = 10;
			this.cboState.Text = null;
			this.cboState.Visible = false;
			// 
			// fraMap
			// 
			this.fraMap.Controls.Add(this.btnDel);
			this.fraMap.Controls.Add(this.btnEdit);
			this.fraMap.Controls.Add(this.grdMapView);
			this.fraMap.Dock = System.Windows.Forms.DockStyle.Top;
			this.fraMap.Location = new System.Drawing.Point(3, 3);
			this.fraMap.Name = "fraMap";
			this.fraMap.Size = new System.Drawing.Size(852, 168);
			this.fraMap.TabIndex = 18;
			this.fraMap.TabStop = false;
			this.fraMap.Text = "Map";
			// 
			// btnDel
			// 
			this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDel.BackColor = System.Drawing.SystemColors.Control;
			this.btnDel.Enabled = false;
			this.btnDel.Location = new System.Drawing.Point(747, 58);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(96, 24);
			this.btnDel.TabIndex = 5;
			this.btnDel.Text = "&Delete";
			this.btnDel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.BackColor = System.Drawing.SystemColors.Control;
			this.btnEdit.Enabled = false;
			this.btnEdit.Location = new System.Drawing.Point(747, 28);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(96, 24);
			this.btnEdit.TabIndex = 4;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// grdMapView
			// 
			this.grdMapView.AllowDrop = true;
			this.grdMapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdMapView.ContextMenu = this.ctxMap;
			this.grdMapView.DataMember = "PostalCodeMappingTable3";
			this.grdMapView.DataSource = this.m_dsMappingView;
			this.grdMapView.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance7.BackColor = System.Drawing.Color.White;
			appearance7.FontData.Name = "Arial";
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdMapView.DisplayLayout.Appearance = appearance7;
			ultraGridColumn38.Hidden = true;
			ultraGridColumn39.Hidden = true;
			ultraGridColumn40.Hidden = true;
			ultraGridColumn41.Header.Caption = "Postal Code";
			ultraGridColumn41.Width = 113;
			ultraGridColumn42.Hidden = true;
			ultraGridColumn43.Width = 60;
			ultraGridColumn44.Header.Caption = "State";
			ultraGridColumn44.Width = 46;
			ultraGridColumn45.Hidden = true;
			ultraGridColumn46.Header.Caption = "Tsort Path";
			ultraGridColumn46.Width = 75;
			ultraGridColumn47.Hidden = true;
			ultraGridColumn48.Hidden = true;
			ultraGridColumn49.Header.Caption = "Tsort Svc";
			ultraGridColumn49.Width = 75;
			ultraGridColumn50.Hidden = true;
			ultraGridColumn51.Header.Caption = "Retrn Path";
			ultraGridColumn51.Width = 75;
			ultraGridColumn52.Hidden = true;
			ultraGridColumn53.Hidden = true;
			ultraGridColumn54.Header.Caption = "Retrn Svc";
			ultraGridColumn54.Width = 75;
			ultraGridColumn55.Hidden = true;
			ultraGridColumn56.Hidden = true;
			ultraGridColumn57.Hidden = true;
			ultraGridColumn58.Hidden = true;
			ultraGridBand2.Columns.Add(ultraGridColumn38);
			ultraGridBand2.Columns.Add(ultraGridColumn39);
			ultraGridBand2.Columns.Add(ultraGridColumn40);
			ultraGridBand2.Columns.Add(ultraGridColumn41);
			ultraGridBand2.Columns.Add(ultraGridColumn42);
			ultraGridBand2.Columns.Add(ultraGridColumn43);
			ultraGridBand2.Columns.Add(ultraGridColumn44);
			ultraGridBand2.Columns.Add(ultraGridColumn45);
			ultraGridBand2.Columns.Add(ultraGridColumn46);
			ultraGridBand2.Columns.Add(ultraGridColumn47);
			ultraGridBand2.Columns.Add(ultraGridColumn48);
			ultraGridBand2.Columns.Add(ultraGridColumn49);
			ultraGridBand2.Columns.Add(ultraGridColumn50);
			ultraGridBand2.Columns.Add(ultraGridColumn51);
			ultraGridBand2.Columns.Add(ultraGridColumn52);
			ultraGridBand2.Columns.Add(ultraGridColumn53);
			ultraGridBand2.Columns.Add(ultraGridColumn54);
			ultraGridBand2.Columns.Add(ultraGridColumn55);
			ultraGridBand2.Columns.Add(ultraGridColumn56);
			ultraGridBand2.Columns.Add(ultraGridColumn57);
			ultraGridBand2.Columns.Add(ultraGridColumn58);
			ultraGridBand2.Columns.Add(ultraGridColumn59);
			appearance8.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance8.FontData.Name = "Verdana";
			appearance8.FontData.SizeInPoints = 8F;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.ActiveRowAppearance = appearance8;
			appearance9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance9.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance9.FontData.Name = "Verdana";
			appearance9.FontData.SizeInPoints = 8F;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance9.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand2.Override.HeaderAppearance = appearance9;
			appearance10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance10.FontData.Name = "Verdana";
			appearance10.FontData.SizeInPoints = 8F;
			appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.RowAlternateAppearance = appearance10;
			appearance11.BackColor = System.Drawing.Color.White;
			appearance11.FontData.Name = "Verdana";
			appearance11.FontData.SizeInPoints = 8F;
			appearance11.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance11.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand2.Override.RowAppearance = appearance11;
			ultraGridColumn60.Hidden = true;
			ultraGridColumn61.Hidden = true;
			ultraGridColumn62.Hidden = true;
			ultraGridColumn63.Header.Caption = "Postal Code";
			ultraGridColumn63.Width = 94;
			ultraGridColumn64.Hidden = true;
			ultraGridColumn65.Width = 60;
			ultraGridColumn66.Header.Caption = "State";
			ultraGridColumn66.Width = 46;
			ultraGridColumn67.Hidden = true;
			ultraGridColumn68.Header.Caption = "Tsort Path";
			ultraGridColumn68.Width = 75;
			ultraGridColumn69.Hidden = true;
			ultraGridColumn70.Hidden = true;
			ultraGridColumn71.Header.Caption = "Tsort Svc";
			ultraGridColumn71.Width = 75;
			ultraGridColumn72.Hidden = true;
			ultraGridColumn73.Header.Caption = "Retrn Path";
			ultraGridColumn73.Width = 75;
			ultraGridColumn74.Hidden = true;
			ultraGridColumn75.Hidden = true;
			ultraGridColumn76.Header.Caption = "Retrn Svc";
			ultraGridColumn76.Width = 75;
			ultraGridColumn77.Hidden = true;
			ultraGridColumn78.Hidden = true;
			ultraGridColumn79.Hidden = true;
			ultraGridColumn80.Hidden = true;
			ultraGridColumn81.Hidden = true;
			ultraGridBand3.Columns.Add(ultraGridColumn60);
			ultraGridBand3.Columns.Add(ultraGridColumn61);
			ultraGridBand3.Columns.Add(ultraGridColumn62);
			ultraGridBand3.Columns.Add(ultraGridColumn63);
			ultraGridBand3.Columns.Add(ultraGridColumn64);
			ultraGridBand3.Columns.Add(ultraGridColumn65);
			ultraGridBand3.Columns.Add(ultraGridColumn66);
			ultraGridBand3.Columns.Add(ultraGridColumn67);
			ultraGridBand3.Columns.Add(ultraGridColumn68);
			ultraGridBand3.Columns.Add(ultraGridColumn69);
			ultraGridBand3.Columns.Add(ultraGridColumn70);
			ultraGridBand3.Columns.Add(ultraGridColumn71);
			ultraGridBand3.Columns.Add(ultraGridColumn72);
			ultraGridBand3.Columns.Add(ultraGridColumn73);
			ultraGridBand3.Columns.Add(ultraGridColumn74);
			ultraGridBand3.Columns.Add(ultraGridColumn75);
			ultraGridBand3.Columns.Add(ultraGridColumn76);
			ultraGridBand3.Columns.Add(ultraGridColumn77);
			ultraGridBand3.Columns.Add(ultraGridColumn78);
			ultraGridBand3.Columns.Add(ultraGridColumn79);
			ultraGridBand3.Columns.Add(ultraGridColumn80);
			ultraGridBand3.Columns.Add(ultraGridColumn81);
			appearance12.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance12.FontData.Name = "Verdana";
			appearance12.FontData.SizeInPoints = 8F;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.ActiveRowAppearance = appearance12;
			appearance13.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance13.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance13.FontData.Name = "Verdana";
			appearance13.FontData.SizeInPoints = 8F;
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance13.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.HeaderAppearance = appearance13;
			appearance14.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance14.FontData.Name = "Verdana";
			appearance14.FontData.SizeInPoints = 8F;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.RowAlternateAppearance = appearance14;
			appearance15.BackColor = System.Drawing.Color.White;
			appearance15.FontData.Name = "Verdana";
			appearance15.FontData.SizeInPoints = 8F;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance15.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.RowAppearance = appearance15;
			this.grdMapView.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			this.grdMapView.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
			appearance16.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance16.FontData.Name = "Verdana";
			appearance16.FontData.SizeInPoints = 8F;
			appearance16.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance16.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdMapView.DisplayLayout.CaptionAppearance = appearance16;
			this.grdMapView.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdMapView.DisplayLayout.InterBandSpacing = 3;
			this.grdMapView.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdMapView.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdMapView.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdMapView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdMapView.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.CheckOnDisplay;
			this.grdMapView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdMapView.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdMapView.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdMapView.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdMapView.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdMapView.Location = new System.Drawing.Point(6, 28);
			this.grdMapView.Name = "grdMapView";
			this.grdMapView.Size = new System.Drawing.Size(732, 131);
			this.grdMapView.TabIndex = 0;
			this.grdMapView.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
			this.grdMapView.SelectionDrag += new System.ComponentModel.CancelEventHandler(this.OnMapSelectionDrag);
			this.grdMapView.DoubleClick += new System.EventHandler(this.OnMapDoubleClicked);
			this.grdMapView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMapMouseDown);
			this.grdMapView.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnMapSelectionChanged);
			this.grdMapView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMapMouseUp);
			this.grdMapView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMapMouseMove);
			this.grdMapView.DragLeave += new System.EventHandler(this.OnMapDragLeave);
			// 
			// ctxMap
			// 
			this.ctxMap.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.ctxMapEdit,
																				   this.ctxMapDelete});
			// 
			// ctxMapEdit
			// 
			this.ctxMapEdit.Index = 0;
			this.ctxMapEdit.Text = "&Edit";
			this.ctxMapEdit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxMapDelete
			// 
			this.ctxMapDelete.Index = 1;
			this.ctxMapDelete.Text = "&Delete";
			this.ctxMapDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// m_dsMappingView
			// 
			this.m_dsMappingView.DataSetName = "MapDS";
			this.m_dsMappingView.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mMapViewDS
			// 
			this.mMapViewDS.DataSetName = "MapDS";
			this.mMapViewDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.BackColor = System.Drawing.SystemColors.Control;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(759, 427);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 26;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(387, 36);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 21);
			this.chkStatus.TabIndex = 21;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// cboSortCenter
			// 
			this.cboSortCenter.DataSource = this.mTerminalsDS;
			this.cboSortCenter.DisplayMember = "SelectionListTable.Description";
			this.cboSortCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortCenter.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboSortCenter.Location = new System.Drawing.Point(111, 9);
			this.cboSortCenter.Name = "cboSortCenter";
			this.cboSortCenter.Size = new System.Drawing.Size(174, 21);
			this.cboSortCenter.TabIndex = 24;
			this.cboSortCenter.ValueMember = "SelectionListTable.ID";
			this.cboSortCenter.SelectionChangeCommitted += new System.EventHandler(this.OnSortCenterChanged);
			// 
			// _lblDesc
			// 
			this._lblDesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblDesc.Location = new System.Drawing.Point(285, 9);
			this._lblDesc.Name = "_lblDesc";
			this._lblDesc.Size = new System.Drawing.Size(96, 18);
			this._lblDesc.TabIndex = 22;
			this._lblDesc.Text = "Description";
			this._lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblClient
			// 
			this._lblClient.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblClient.Location = new System.Drawing.Point(9, 36);
			this._lblClient.Name = "_lblClient";
			this._lblClient.Size = new System.Drawing.Size(96, 18);
			this._lblClient.TabIndex = 20;
			this._lblClient.Text = "Client";
			this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblSortCenter
			// 
			this._lblSortCenter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblSortCenter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblSortCenter.Location = new System.Drawing.Point(9, 9);
			this._lblSortCenter.Name = "_lblSortCenter";
			this._lblSortCenter.Size = new System.Drawing.Size(96, 18);
			this._lblSortCenter.TabIndex = 23;
			this._lblSortCenter.Text = "Sort Center";
			this._lblSortCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDesc
			// 
			this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtDesc.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtDesc.Location = new System.Drawing.Point(387, 9);
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.Size = new System.Drawing.Size(465, 21);
			this.txtDesc.TabIndex = 17;
			this.txtDesc.Text = "";
			this.txtDesc.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// cboClient
			// 
			this.cboClient.DataSource = this.mClientsDS;
			this.cboClient.DisplayMember = "SelectionListTable.Description";
			this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboClient.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboClient.Location = new System.Drawing.Point(111, 36);
			this.cboClient.Name = "cboClient";
			this.cboClient.Size = new System.Drawing.Size(174, 21);
			this.cboClient.TabIndex = 19;
			this.cboClient.ValueMember = "SelectionListTable.ID";
			this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
			// 
			// btnApply
			// 
			this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnApply.BackColor = System.Drawing.SystemColors.Control;
			this.btnApply.Enabled = false;
			this.btnApply.Location = new System.Drawing.Point(657, 427);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(96, 24);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "&Apply";
			this.btnApply.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// mTsortPathsDS
			// 
			this.mTsortPathsDS.DataSetName = "SelectionList";
			this.mTsortPathsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mReturnPathsDS
			// 
			this.mReturnPathsDS.DataSetName = "SelectionList";
			this.mReturnPathsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mTsortServicesDS
			// 
			this.mTsortServicesDS.DataSetName = "SelectionList";
			this.mTsortServicesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mReturnServicesDS
			// 
			this.mReturnServicesDS.DataSetName = "SelectionList";
			this.mReturnServicesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mCountriesDS
			// 
			this.mCountriesDS.DataSetName = "CountryDS";
			this.mCountriesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mStatesDS
			// 
			this.mStatesDS.DataSetName = "StateDS";
			this.mStatesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgMapDetail
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(858, 455);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this._lblDesc);
			this.Controls.Add(this._lblClient);
			this.Controls.Add(this._lblSortCenter);
			this.Controls.Add(this.txtDesc);
			this.Controls.Add(this.cboClient);
			this.Controls.Add(this.chkStatus);
			this.Controls.Add(this.cboSortCenter);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgMapDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Map Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mClientsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTerminalsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mMapDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mMappingsDS)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.fraOverrides.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdOverride)).EndInit();
			this.fraMap.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdMapView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_dsMappingView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mMapViewDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortPathsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnPathsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTsortServicesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mReturnServicesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial service states
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mTerminalsDS.Merge(EnterpriseFactory.GetEntTerminals());
				if(this.mAction!=MapActionEnum.MapActionView) {
					this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
					this.mStatesDS.Merge(EnterpriseFactory.GetStates());
					this.mTsortServicesDS.Merge(EnterpriseFactory.GetRegularOutboundServiceTypes());
					this.mReturnServicesDS.Merge(EnterpriseFactory.GetReturnOutboundServiceTypes());
				}
				
				//Load control data	from detail dataset mMapDS		
				#region Default grid behavior
				this.grdMapView.DisplayLayout.Bands[0].Columns["PostalCode"].SortIndicator = SortIndicator.Ascending;
				this.grdMapView.DisplayLayout.Bands[1].Columns["PostalCode"].SortIndicator = SortIndicator.Ascending;
				this.grdOverride.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdOverride.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdOverride.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["SortCenter"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ClientName"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PostalCode"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["CountryID"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["StateOrProvince"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldTsortPathMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldTsortServiceMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldReturnPathMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["OldReturnServiceMnemonic"].CellActivation = Activation.NoEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDTsort"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDTsort"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDReturns"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDReturns"].CellActivation = Activation.AllowEdit;
				this.grdOverride.DisplayLayout.Bands[0].Columns["CountryID"].EditorControl = this.cboCountry;
				this.grdOverride.DisplayLayout.Bands[0].Columns["StateOrProvince"].EditorControl = this.cboState;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDTsort"].EditorControl = this.cboTsortPath;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDTsort"].EditorControl = this.cboTsortService;
				this.grdOverride.DisplayLayout.Bands[0].Columns["PathIDReturns"].EditorControl = this.cboReturnPath;
				this.grdOverride.DisplayLayout.Bands[0].Columns["ServiceIDReturns"].EditorControl = this.cboReturnService;
				//this.grdOverride.DisplayLayout.Bands[0].Columns["PostalCode"].SortIndicator = SortIndicator.Ascending;
				#endregion
				if(!this.mMapDS.MapDetailTable[0].IsSortCenterIDNull()) 
					this.cboSortCenter.SelectedValue = this.mMapDS.MapDetailTable[0].SortCenterID;
				else
					if(this.cboSortCenter.Items.Count>0) this.cboSortCenter.SelectedIndex = 0;
				OnSortCenterChanged(null, null);
				this.txtDesc.MaxLength = 40;
				this.txtDesc.Text = this.mMapDS.MapDetailTable[0].Description.Trim();
				if(!this.mMapDS.MapDetailTable[0].IsClientIDNull()) 
					this.cboClient.SelectedValue = this.mMapDS.MapDetailTable[0].ClientID;
				else {
					if(this.mType==MapTypeEnum.MapTypeClient) 
						if(this.cboClient.Items.Count>0) this.cboClient.SelectedIndex = 0;
				}
				this.chkStatus.Checked = this.mMapDS.MapDetailTable[0].IsActive;
				this.chkStatus.Enabled = (this.mType==MapTypeEnum.MapTypeClient);
				
				//Set UI configuration
				switch(this.mAction) {
					case MapActionEnum.MapActionCreate:
						//Hide grdMapView- new mappings in grdOverride (initial records: none)
						this.cboSortCenter.Enabled = false;
						this.cboClient.Enabled = (this.cboClient.Items.Count>0);
						this.fraMap.Height = 84;
						this.fraOverrides.Top = 90;
						this.fraOverrides.Height = this.pnlMain.Height - 96;
						this.btnAdd.Enabled = true;
						break;
					case MapActionEnum.MapActionCopy:
                        //Hide grdMapView- copied mappings in grdOverride (initial records: from copy)
						this.cboSortCenter.Enabled = (this.cboSortCenter.Items.Count>0);
						this.cboClient.Enabled = (this.cboClient.Items.Count>0 && (this.mType==MapTypeEnum.MapTypeClient));
						this.fraMap.Height = 84;
						this.fraOverrides.Top = 90;
						this.fraOverrides.Height = this.pnlMain.Height - 96;
						this.btnAdd.Enabled = true;
						break;
					case MapActionEnum.MapActionEdit:
						//View mappings in grdMapView; edit mappings in grdOverride (initial records: none)
						this.cboSortCenter.Enabled = this.cboClient.Enabled = false;
						this.fraMap.Height = 168;
						this.fraOverrides.Top = 190;
						this.fraOverrides.Height = 168;
						this.btnAdd.Enabled = true;
						break;
					case MapActionEnum.MapActionView:
						//Hide grdOverride- all mappings read-only in grdMapView from mMapViewDS
						this.cboSortCenter.Enabled = this.cboClient.Enabled = this.txtDesc.Enabled = this.chkStatus.Enabled = false;
						this.splitterH.Visible = this.fraOverrides.Visible = false;
						this.btnEdit.Visible = this.btnDel.Visible = false;
						this.grdMapView.Width = this.fraMap.Width - 12;
						this.fraMap.Height = this.pnlMain.Height - 12;
						break;
				}
				if(this.grdMapView.Rows.Count>0) this.grdMapView.ActiveRow = this.grdMapView.Rows[0];
				if(this.grdOverride.Rows.Count>0) this.grdOverride.ActiveRow = this.grdOverride.Rows[0];
				
				#region UltraComboEditor combo boxes don't support binding- populate manually from dataset
				this.cboCountry.Items.Clear();
				for(int i=0; i<this.mCountriesDS.CountryDetailTable.Rows.Count; i++) 
					this.cboCountry.Items.Add(this.mCountriesDS.CountryDetailTable[i].CountryID, this.mCountriesDS.CountryDetailTable[i].Country);
				this.cboCountry.Enabled = (this.cboCountry.Items.Count>0);
				
				this.cboState.Items.Clear();
				for(int i=0; i<this.mStatesDS.StateListTable.Rows.Count; i++) 
					this.cboState.Items.Add(this.mStatesDS.StateListTable[i].STATE, this.mStatesDS.StateListTable[i].STATE);
				this.cboState.Enabled = (this.cboState.Items.Count>0);
				
				this.cboTsortService.Items.Clear();
				for(int i=0; i<this.mTsortServicesDS.SelectionListTable.Rows.Count; i++) 
					this.cboTsortService.Items.Add(this.mTsortServicesDS.SelectionListTable[i].ID, this.mTsortServicesDS.SelectionListTable[i].Description);
				this.cboTsortService.Enabled = (this.cboTsortService.Items.Count>0);
				
				this.cboReturnService.Items.Clear();
				for(int i=0; i<this.mReturnServicesDS.SelectionListTable.Rows.Count; i++) 
					this.cboReturnService.Items.Add(this.mReturnServicesDS.SelectionListTable[i].ID, this.mReturnServicesDS.SelectionListTable[i].Description);
				this.cboReturnService.Enabled = (this.cboReturnService.Items.Count>0);
				#endregion
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnApply.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnSortCenterChanged(object sender, System.EventArgs e) {
			//Event handler for change in selected sort center
			//Debug.Write("OnSortCenterChanged()\n");
			int sortCenterID=0;
			string sSortCenter="";
			try {
				//Determine Sort Center
				sortCenterID = Convert.ToInt32(this.cboSortCenter.SelectedValue);
				sSortCenter = this.cboSortCenter.Text;
				
				//Get clients that are associated with this terminal
				if(this.mType==MapTypeEnum.MapTypeClient) {
					this.mClientsDS.Clear();
					this.mClientsDS.Merge(EnterpriseFactory.GetClients(sortCenterID));
				}

				//Get valid Tsort freight paths for this sort center
				this.cboTsortPath.Items.Clear();
				this.mTsortPathsDS.Merge(EnterpriseFactory.GetFreightPaths(sortCenterID));
				for(int i=0; i<this.mTsortPathsDS.SelectionListTable.Rows.Count; i++) 
					this.cboTsortPath.Items.Add(this.mTsortPathsDS.SelectionListTable[i].ID, this.mTsortPathsDS.SelectionListTable[i].Description);
				this.cboTsortPath.Enabled = (this.cboTsortPath.Items.Count>0);
				
				//Get valid return freight paths for this sort center
				this.cboReturnPath.Items.Clear();
				this.mReturnPathsDS.Merge(EnterpriseFactory.GetFreightPaths(sortCenterID));
				for(int i=0; i<this.mReturnPathsDS.SelectionListTable.Rows.Count; i++) 
					this.cboReturnPath.Items.Add(this.mReturnPathsDS.SelectionListTable[i].ID, this.mReturnPathsDS.SelectionListTable[i].Description);
				this.cboReturnPath.Enabled = (this.cboReturnPath.Items.Count>0);
				
				//Update mappings and freight path selections
				for(int i=0; i<this.mMappingsDS.PostalCodeMappingTable.Rows.Count; i++) {
					this.mMappingsDS.PostalCodeMappingTable[i].SortCenter = sSortCenter;
					this.mMappingsDS.PostalCodeMappingTable[i].PathIDTsort = (this.mTsortPathsDS.SelectionListTable.Count>0) ? this.mTsortPathsDS.SelectionListTable[0].ID : "";
					this.mMappingsDS.PostalCodeMappingTable[i].PathIDReturns = (this.mReturnPathsDS.SelectionListTable.Count>0) ? this.mReturnPathsDS.SelectionListTable[0].ID : "";
				}
				this.grdOverride.Refresh();
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnClientChanged(object sender, System.EventArgs e) {
			//Event handler for change in selected client
			string sClient="";
			try {
				//Applies to: view-N; create-Y; copy-Y; edit-N
				if(this.mAction==MapActionEnum.MapActionCreate || this.mAction==MapActionEnum.MapActionCopy) {
					//Copy selected value to all mappings
					sClient = this.cboClient.Text;
					for(int i=0; i<this.mMappingsDS.PostalCodeMappingTable.Rows.Count; i++) 
						this.mMappingsDS.PostalCodeMappingTable[i].ClientName = sClient;
				}
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region MapDS Grid: OnMapSelectionChanged(), OnMapDoubleClicked(), OnMapMouseDown(), OnMapMouseMove(), OnMapMouseUp(), OnMapSelectionDrag(), OnMapDragLeave()
		private void OnMapSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for mapping cell activated
			//Debug.Write("OnMapSelectionChanged()\n");
			string sPostalCode="", sCountry="";
			DataRow[] rowExists=null;
			try {
				//Set service states
				sPostalCode = this.grdMapView.Selected.Rows[0].Cells["PostalCode"].Value.ToString();
				sCountry = this.grdMapView.Selected.Rows[0].Cells["Country"].Value.ToString();
				rowExists = this.mMappingsDS.PostalCodeMappingTable.Select("PostalCode='" + sPostalCode + "'");
				if(rowExists.Length==0 && (this.mAction==MapActionEnum.MapActionEdit)) {
					//Edit an existing mapping: 3\5 position terminal\client mappings
					this.btnEdit.Enabled = ((sCountry=="USA") && (sPostalCode.Length==3 || sPostalCode.Length==5)) || ((sCountry!="USA") && (sPostalCode.Length>0));
					
					//Delete an existing mapping: only 5 position terminal; 3\5 position client mappings
					if(this.mType==MapTypeEnum.MapTypeClient)
						this.btnDel.Enabled = ((sCountry=="USA") && (sPostalCode.Length==3 || sPostalCode.Length==5)) || ((sCountry!="USA") && (sPostalCode.Length>0));
					else
						this.btnDel.Enabled = ((sCountry=="USA") && (sPostalCode.Length==5)) || ((sCountry!="USA") && (sPostalCode.Length>0));
				}
				else 
					this.btnEdit.Enabled = this.btnDel.Enabled = false;
			} 
			catch(Exception ex) { reportError(ex); }
		}

		private void OnMapDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for double-clicking a mapping
			try {
				//Edit the selected record
				if(this.btnEdit.Enabled) OnCmdClick(this.btnEdit, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMapMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for ...
			try {
				//Flag user may begin dragging
				this.mIsDragging = (e.Button==MouseButtons.Left);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMapMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for ...
			DataObject oData=null;
			try {
				//Start drag\drop if user is dragging
				switch(e.Button) {
					case MouseButtons.Left:
						if(this.mIsDragging) {
							oData = new DataObject();
							if(this.grdMapView.Selected.Rows.Count>0) {
								oData.SetData("");
								this.grdMapView.DoDragDrop(oData, DragDropEffects.Copy);
							}
						}
						break;
				}

			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMapMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for ...
			this.mIsDragging = false;
		}
		private void OnMapSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) {        
			//Event handler for ...
			e.Cancel = !this.mIsDragging;
		}
		private void OnMapDragLeave(object sender, System.EventArgs e) { }
		#endregion
		#region Mappings Grid: OnBeforeMappingCellActivated(), OnMappingCellChanged(), OnMappingRowUpdated(), OnMappingSelectionChanged(), OnMappingDragEnter(), OnMappingDragOver(), OnMappingDragDrop()
		private void OnBeforeMappingCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for mapping cell activated
			//Debug.Write("OnBeforeMappingCellActivated()\n");
			bool bMapping_Add=false, bMapping_Rem=false;
			try {
				//
				bMapping_Add = (e.Cell.Row.Cells["RowAction"].Value.ToString()=="A");
				bMapping_Rem = (e.Cell.Row.Cells["RowAction"].Value.ToString()=="D");
				switch(e.Cell.Column.Key.ToString()) {
					case "PostalCode":		e.Cell.Activation =  bMapping_Add ? Activation.AllowEdit : Activation.NoEdit; break;
					case "CountryID":		e.Cell.Activation =  bMapping_Add ? Activation.AllowEdit : Activation.NoEdit; break;
					case "StateOrProvince":	e.Cell.Activation =  bMapping_Add ? Activation.AllowEdit : Activation.NoEdit; break;
					case "PathIDTsort":		e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; break;
					case "ServiceIDTsort":	e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; break;
					case "PathIDReturns":	e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; break;
					case "ServiceIDReturns":e.Cell.Activation = !bMapping_Rem ? Activation.AllowEdit : Activation.NoEdit; break;
					default:				e.Cell.Activation = Activation.NoEdit; break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingCellChanged(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a mapping cell value
			try {
				//Validate cell values
				switch(e.Cell.Column.Key.ToString()) {
					case "PostalCode":																			this.grdOverride.UpdateData(); break;
					case "CountryID":			e.Cell.Row.Cells["Country"].Value = e.Cell.Text;				this.grdOverride.UpdateData(); break;
					case "StateOrProvince":																		this.grdOverride.UpdateData(); break;
					case "PathIDTsort":			e.Cell.Row.Cells["TsortPathMnemonic"].Value = e.Cell.Text;		this.grdOverride.UpdateData(); break;
					case "ServiceIDTsort":		e.Cell.Row.Cells["TsortServiceMnemonic"].Value = e.Cell.Text;	this.grdOverride.UpdateData(); break;
					case "PathIDReturns":		e.Cell.Row.Cells["ReturnPathMnemonic"].Value = e.Cell.Text;		this.grdOverride.UpdateData(); break;
					case "ServiceIDReturns":	e.Cell.Row.Cells["ReturnServiceMnemonic"].Value = e.Cell.Text;	this.grdOverride.UpdateData(); break;
					default:					Debug.Write(": value=" + e.Cell.Value.ToString()); break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}		
		private void OnMappingRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in a mapping row
			//Validate row
			ValidateForm(null, null);
		}
		
		private void OnMappingSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for mapping cell activated
			try {
				//Allow remove if there is a selection
				this.btnRem.Enabled = (this.mAction!=MapActionEnum.MapActionView);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingDragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging a mapping into the grid
			DataObject oData=null;
			try {
				//On drag enter, turn on copy drag drop effect
				oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true))
					e.Effect = DragDropEffects.Copy;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging a mapping over the grid
			DataObject oData=null;
			try {
				//Retrieve drag drop data
				oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true)) 
					e.Effect = DragDropEffects.Copy;
				else
					e.Effect = DragDropEffects.None;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnMappingDragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dropping a mapping onto the grid
			DataObject oData=null;
			try {
				//Retrieve data
				oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.StringFormat, true)) {
					//
					if(this.btnEdit.Enabled) OnCmdClick(this.btnEdit, null);
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region User Services: ValidateForm(), OnCmdClick(), OnMenuClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			bool bMappingsValid, bCodeValid=false;;
			Object oCell=null;
			string sRowAction="", sPostalCode="", sCountry="", sState="", sTFP="", sRFP="";
			int iCntryID=0, iTST=0, iRST=0;
			
			try {
				//Validate mappings (postal code, country, state, freight paths, and service types)
				bMappingsValid = true;
				for(int i=0; i<this.grdOverride.Rows.Count; i++) {
					sRowAction = this.grdOverride.Rows[i].Cells["RowAction"].Value.ToString();
					if(sRowAction!="D") {
						//Validate new and edit records (delete are valid)
						sPostalCode = this.grdOverride.Rows[i].Cells["PostalCode"].Value.ToString();
						iCntryID = Convert.ToInt32(this.grdOverride.Rows[i].Cells["CountryID"].Value);
						sCountry = this.grdOverride.Rows[i].Cells["Country"].Value.ToString();
						sState = this.grdOverride.Rows[i].Cells["StateOrProvince"].Value.ToString();
						sTFP = this.grdOverride.Rows[i].Cells["PathIDTsort"].Value.ToString();
						oCell = this.grdOverride.Rows[i].Cells["ServiceIDTsort"].Value;
						iTST = (oCell!=DBNull.Value) ? Convert.ToInt32(oCell) : 0;
						sRFP = this.grdOverride.Rows[i].Cells["PathIDReturns"].Value.ToString();
						oCell = this.grdOverride.Rows[i].Cells["ServiceIDReturns"].Value;
						iRST = (oCell!=DBNull.Value) ? Convert.ToInt32(oCell) : 0;

						//Validate [new] postal codes
						if(sRowAction=="A") {
							if(sCountry=="USA")
								bCodeValid = ((this.mType==MapTypeEnum.MapTypeTerminal && sPostalCode.Length==5) || 
									(this.mType==MapTypeEnum.MapTypeClient && (sPostalCode.Length==3 || sPostalCode.Length==5)));
							else
								bCodeValid = (sPostalCode.Length>0 && sPostalCode.Length<=10);
						}
						else
							bCodeValid = true;
						
						//Verify country, state, freight paths, and service types
						bMappingsValid = (bCodeValid && iCntryID>0 && (sCountry!="USA" || sState!="") && ((sTFP!="" && iTST>0) && (sRFP!="" && iRST>0)));
					}
				}
				
				
				//Enable OK service if details have valid changes
				this.btnApply.Enabled = (this.mAction!=MapActionEnum.MapActionView && this.cboSortCenter.Text!="" && 
					(this.mType==MapTypeEnum.MapTypeTerminal || this.cboClient.Text!="") &&
					this.txtDesc.Text!="" && bMappingsValid);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			MapDS.PostalCodeMappingTableRow _rowMapping;
			string sRowAction="", mapID="";
			int iMappings=0;
			MapDS dsMappings=null;
			bool bUpdated=false;
			try {
				this.Cursor = Cursors.WaitCursor;
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_EDIT: 
						this.ctxMapEdit.PerformClick();
						break;
					case CMD_DEL: 
						this.ctxMapDelete.PerformClick();
						break;
					case CMD_ADD: 
						//Add a default row
						_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
						#region Copy field-by-field
						_rowMapping.RowAction = "A";
						_rowMapping.MapID = this.mMapDS.MapDetailTable[0].MapID;
						_rowMapping.SortCenter = this.cboSortCenter.Text;
						_rowMapping.ClientName = this.cboClient.Text;
						_rowMapping.PostalCode = "";
						_rowMapping.CountryID = this.mCountriesDS.CountryDetailTable[1].CountryID;
						_rowMapping.Country = this.mCountriesDS.CountryDetailTable[1].Country;
						_rowMapping.StateOrProvince = this.mStatesDS.StateListTable[0].STATE;
						//_rowMapping.PathIDTsort =  "";
						//_rowMapping.TsortPathMnemonic = "";
						//_rowMapping.ServiceIDTsort = 0;
						//_rowMapping.TsortServiceMnemonic = "";
						//_rowMapping.PathIDReturns = "";
						//_rowMapping.ReturnPathMnemonic = "";
						//_rowMapping.ServiceIDReturns = 0;
						//_rowMapping.ReturnServiceMnemonic = "";
						_rowMapping.LastUpdated = DateTime.Now;
						_rowMapping.UserID = System.Environment.UserName;
						_rowMapping.RowVersion = "";
						_rowMapping.Status = "";
						#endregion
						this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
						break;
					case CMD_REM: 
						//Remove this record from the (transactional) mappings grid
						if(this.grdOverride.ActiveRow!=null) this.grdOverride.ActiveRow.Selected = true;
						this.grdOverride.Selected.Rows[0].Delete(false);
						this.grdOverride.Update();
						this.mMappingsDS.AcceptChanges();
						break;
					case CMD_APPLY: 
						//Update map header and create\update\delete mappings
						this.mMapDS.MapDetailTable[0].Description = this.txtDesc.Text;
						this.mMapDS.MapDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mMapDS.MapDetailTable[0].LastUpdated = DateTime.Now;
						this.mMapDS.MapDetailTable[0].UserID = Environment.UserName;
					switch(this.mAction) {
						case MapActionEnum.MapActionCreate:
							//Create a new client map (header and mappings)
							this.mMapDS.MapDetailTable[0].SortCenterID = Convert.ToInt32(this.cboSortCenter.SelectedValue);
							this.mMapDS.MapDetailTable[0].ClientID = Convert.ToInt32(this.cboClient.SelectedValue);
							mapID = EnterpriseFactory.CreateClientMapHeader(this.mMapDS);
							if(mapID!="") {
								//Apply single creates (all mappings are new)- set mapID
								iMappings = this.mMappingsDS.PostalCodeMappingTable.Rows.Count;
								for(int i=0; i<iMappings; i++) {
									try {
										_rowMapping = this.mMappingsDS.PostalCodeMappingTable[i];
										_rowMapping.MapID = mapID;
										this.mMapDS.PostalCodeMappingTable.Clear();
										this.mMapDS.PostalCodeMappingTable.ImportRow(_rowMapping);
										Debug.Write(i.ToString() + "\n" + this.mMapDS.GetXml() + "\n");
										bUpdated = EnterpriseFactory.CreateMapping(this.mMapDS);
										if(bUpdated) this.mMappingsDS.PostalCodeMappingTable[i].Status="OK";
									}
									catch(Exception exc) {
										this.mMappingsDS.PostalCodeMappingTable[i].Status = exc.Message;
									}
								}

								//Setup for edit mode
								this.mAction = MapActionEnum.MapActionEdit;
								dsMappings = new MapDS();
								dsMappings.Merge(this.mMappingsDS);
								this.mMappingsDS.PostalCodeMappingTable.Clear();
								for(int i=0; i<dsMappings.PostalCodeMappingTable.Rows.Count; i++) {
									if(dsMappings.PostalCodeMappingTable[i].Status!="OK") 
										this.mMappingsDS.PostalCodeMappingTable.ImportRow(dsMappings.PostalCodeMappingTable[i]);
								}
								setViewAndDetail(mapID, this.mMapDS.MapDetailTable[0].SortCenterID);
								this.OnFormLoad(this, null);
							}
							break;
						case MapActionEnum.MapActionCopy:
							//Create a new terminal or client map (header and mappings) from a copy
							this.mMapDS.MapDetailTable[0].SortCenterID = Convert.ToInt32(this.cboSortCenter.SelectedValue);
							if(this.mType==MapTypeEnum.MapTypeClient) {
								//Create client map header
								this.mMapDS.MapDetailTable[0].ClientID = Convert.ToInt32(this.cboClient.SelectedValue);
								mapID = EnterpriseFactory.CreateClientMapHeader(this.mMapDS);
								if(mapID!="") {
									//Apply single creates (all mappings are new)- set mapID
									iMappings = this.mMappingsDS.PostalCodeMappingTable.Rows.Count;
									for(int i=0; i<iMappings; i++) {
										try {
											_rowMapping = this.mMappingsDS.PostalCodeMappingTable[i];
											_rowMapping.MapID = mapID;
											this.mMapDS.PostalCodeMappingTable.Clear();
											this.mMapDS.PostalCodeMappingTable.ImportRow(_rowMapping);
											Debug.Write(i.ToString() + "\n" + this.mMapDS.GetXml() + "\n");
											bUpdated = EnterpriseFactory.CreateMapping(this.mMapDS);
											if(bUpdated) this.mMappingsDS.PostalCodeMappingTable[i].Status="OK";
										}
										catch(Exception exc) {
											this.mMappingsDS.PostalCodeMappingTable[i].Status = exc.Message;
										}
									}
										
									//Setup for edit mode
									this.mAction = MapActionEnum.MapActionEdit;
									dsMappings = new MapDS();
									dsMappings.Merge(this.mMappingsDS);
									this.mMappingsDS.PostalCodeMappingTable.Clear();
									for(int i=0; i<dsMappings.PostalCodeMappingTable.Rows.Count; i++) {
										if(dsMappings.PostalCodeMappingTable[i].Status!="OK") 
											this.mMappingsDS.PostalCodeMappingTable.ImportRow(dsMappings.PostalCodeMappingTable[i]);
									}
									setViewAndDetail(mapID, this.mMapDS.MapDetailTable[0].SortCenterID);
									this.OnFormLoad(this, null);
								}
							}
							else {
								//Terminal map- apply batch creates (all mappings are new)- mapID set by middle tier
								this.mMapDS.MapDetailTable[0].SetClientIDNull();
								this.mMapDS.Merge(this.mMappingsDS, true, MissingSchemaAction.Ignore);
								Debug.Write("MapActionCopy\n" + this.mMapDS.GetXml() + "\n");
								try {
									mapID = EnterpriseFactory.CreateTerminalMap(this.mMapDS);
									if(mapID!="") {
										MessageBox.Show("Created terminal map " + mapID + " from copy.");
											
										//Setup for edit mode
										this.mAction = MapActionEnum.MapActionEdit;
										this.mMappingsDS.PostalCodeMappingTable.Clear();
										setViewAndDetail(mapID, this.mMapDS.MapDetailTable[0].SortCenterID);
										this.OnFormLoad(this, null);
									}
								}
								catch(Exception exc) {
									MessageBox.Show("Failed to create terminal map from copy- " + exc.Message + ".");
								}
							}
							break;
						case MapActionEnum.MapActionEdit:
							//Update an exisiting terminal or client map (header and mappings)
							bUpdated = EnterpriseFactory.UpdateMapHeader(this.mMapDS);
							if(bUpdated) {
								//Apply single updates (create, edit, delete)- set mapID for new
								iMappings = this.mMappingsDS.PostalCodeMappingTable.Rows.Count;
								for(int i=0; i<iMappings; i++) {
									try {
										//New, edit, delete?
										_rowMapping = this.mMappingsDS.PostalCodeMappingTable[i];
										sRowAction = _rowMapping.RowAction;		//Capture flag
										this.mMapDS.PostalCodeMappingTable.Clear();
										switch(sRowAction) {
											case "A": 
												this.mMapDS.PostalCodeMappingTable.ImportRow(_rowMapping);
												Debug.Write(i.ToString() + "-Add\n" + this.mMapDS.GetXml() + "\n");
												bUpdated = EnterpriseFactory.CreateMapping(this.mMapDS); 
												break;
											case "D": 
												Debug.Write(i.ToString() + "-Delete\n" + "mapID=" + _rowMapping.MapID + ", countryID=" + _rowMapping.CountryID.ToString() + ", code=" + _rowMapping.PostalCode + ", rowVer=" + _rowMapping.RowVersion + "\n");
												bUpdated = EnterpriseFactory.DeleteMapping(_rowMapping.MapID, _rowMapping.CountryID, _rowMapping.PostalCode, _rowMapping.RowVersion); 
												break;
											case "E": 
												this.mMapDS.PostalCodeMappingTable.ImportRow(_rowMapping);
												Debug.Write(i.ToString() + "-Edit\n" + this.mMapDS.GetXml() + "\n");
												bUpdated = EnterpriseFactory.UpdateMapping(this.mMapDS); 
												break;
										}
										if(bUpdated) this.mMappingsDS.PostalCodeMappingTable[i].Status="OK";
									}
									catch(Exception exc) {
										this.mMappingsDS.PostalCodeMappingTable[i].Status = exc.Message;
									}
								}

								//Continue with edit mode
								this.mAction = MapActionEnum.MapActionEdit;
								dsMappings = new MapDS();
								dsMappings.Merge(this.mMappingsDS);
								this.mMappingsDS.Clear();
								for(int i=0; i<dsMappings.PostalCodeMappingTable.Rows.Count; i++) {
									if(dsMappings.PostalCodeMappingTable[i].Status!="OK") 
										this.mMappingsDS.PostalCodeMappingTable.ImportRow(dsMappings.PostalCodeMappingTable[i]);
								}
								setViewAndDetail(this.mMapDS.MapDetailTable[0].MapID, this.mMapDS.MapDetailTable[0].SortCenterID);
								this.OnFormLoad(this, null);
							}
							break;
					}
						break;
					case CMD_CLOSE:
						//Close the dialog
						if(this.mAction!=MapActionEnum.MapActionView)
							this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu item clicked-apply selected service
			string sPostalCode="", sCountry="";
			DataRow[] rowExists=null;
			MapDS.PostalCodeMappingTableRow _rowMapping;
			MapDS.PostalCodeMappingTable3Row rowMapping3;
			MapDS.PostalCodeMappingTable5Row rowMapping5;

			try  {
				//Make sure this postal code has no existing record in grdOverride
				sPostalCode = this.grdMapView.Selected.Rows[0].Cells["PostalCode"].Value.ToString();
				sCountry = this.grdMapView.Selected.Rows[0].Cells["Country"].Value.ToString();
				rowExists = this.mMappingsDS.PostalCodeMappingTable.Select("PostalCode='" + sPostalCode + "'");
				if(rowExists.Length==0) {
					MenuItem menu = (MenuItem)sender;
					switch(menu.Text)  {
						case MNU_EDIT:
							//Copy selected row to edit mappings grid
							if(this.mType==MapTypeEnum.MapTypeClient || (sCountry=="USA" && sPostalCode.Length==3) || sCountry!="USA") {
								rowMapping3 = (Tsort.Enterprise.MapDS.PostalCodeMappingTable3Row)this.m_dsMappingView.PostalCodeMappingTable3.Select("PostalCode='" + sPostalCode + "'")[0];
								if(rowMapping3!=null) {
									_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
									#region Copy field-by-field
									_rowMapping.RowAction = "E";
									_rowMapping.MapID = rowMapping3.MapID;
									_rowMapping.SortCenter = rowMapping3.SortCenter;
									_rowMapping.ClientName = rowMapping3.ClientName;
									_rowMapping.PostalCode = rowMapping3.PostalCode;
									_rowMapping.CountryID = rowMapping3.CountryID;
									_rowMapping.Country = rowMapping3.Country;
									_rowMapping.StateOrProvince = rowMapping3.StateOrProvince;
									if(!rowMapping3.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = _rowMapping.PathIDTsort = rowMapping3.PathIDTsort;
									if(!rowMapping3.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = _rowMapping.TsortPathMnemonic = rowMapping3.TsortPathMnemonic;
									if(!rowMapping3.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = _rowMapping.ServiceIDTsort = rowMapping3.ServiceIDTsort;
									if(!rowMapping3.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = _rowMapping.TsortServiceMnemonic = rowMapping3.TsortServiceMnemonic;
									if(!rowMapping3.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = _rowMapping.PathIDReturns = rowMapping3.PathIDReturns;
									if(!rowMapping3.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = _rowMapping.ReturnPathMnemonic = rowMapping3.ReturnPathMnemonic;
									if(!rowMapping3.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = _rowMapping.ServiceIDReturns = rowMapping3.ServiceIDReturns;
									if(!rowMapping3.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = _rowMapping.ReturnServiceMnemonic = rowMapping3.ReturnServiceMnemonic;
									_rowMapping.LastUpdated = DateTime.Now;
									_rowMapping.UserID = Environment.UserName;
									_rowMapping.RowVersion = rowMapping3.RowVersion;
									_rowMapping.Status = "";
									#endregion
									this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
								}
							}
							else {
								rowMapping5 = (Tsort.Enterprise.MapDS.PostalCodeMappingTable5Row)this.m_dsMappingView.PostalCodeMappingTable5.Select("PostalCode='" + sPostalCode + "'")[0];
								if(rowMapping5!=null) {
									_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
									#region Copy field-by-field
									_rowMapping.RowAction = "E";
									_rowMapping.MapID = rowMapping5.MapID;
									_rowMapping.SortCenter = rowMapping5.SortCenter;
									_rowMapping.ClientName = rowMapping5.ClientName;
									_rowMapping.PostalCode = rowMapping5.PostalCode;
									_rowMapping.CountryID = rowMapping5.CountryID;
									_rowMapping.Country = rowMapping5.Country;
									_rowMapping.StateOrProvince = rowMapping5.StateOrProvince;
									if(!rowMapping5.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = _rowMapping.PathIDTsort = rowMapping5.PathIDTsort;
									if(!rowMapping5.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = _rowMapping.TsortPathMnemonic = rowMapping5.TsortPathMnemonic;
									if(!rowMapping5.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = _rowMapping.ServiceIDTsort = rowMapping5.ServiceIDTsort;
									if(!rowMapping5.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = _rowMapping.TsortServiceMnemonic = rowMapping5.TsortServiceMnemonic;
									if(!rowMapping5.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = _rowMapping.PathIDReturns = rowMapping5.PathIDReturns;
									if(!rowMapping5.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = _rowMapping.ReturnPathMnemonic = rowMapping5.ReturnPathMnemonic;
									if(!rowMapping5.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = _rowMapping.ServiceIDReturns = rowMapping5.ServiceIDReturns;
									if(!rowMapping5.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = _rowMapping.ReturnServiceMnemonic = rowMapping5.ReturnServiceMnemonic;
									_rowMapping.LastUpdated = DateTime.Now;
									_rowMapping.UserID = Environment.UserName;
									_rowMapping.RowVersion = rowMapping5.RowVersion;
									_rowMapping.Status = "";
									#endregion
									this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
								}
							}
							this.btnEdit.Enabled = this.btnDel.Enabled = false;
							break;
						case MNU_REMOVE:
							//Copy selected row to edit mappings grid and mark for delete
							if(this.mType==MapTypeEnum.MapTypeClient || (sCountry=="USA" && sPostalCode.Length==3) || sCountry!="USA") {
								rowMapping3 = (Tsort.Enterprise.MapDS.PostalCodeMappingTable3Row)this.m_dsMappingView.PostalCodeMappingTable3.Select("PostalCode='" + sPostalCode + "'")[0];
								if(rowMapping3!=null) {
									_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
									#region Copy field-by-field
									_rowMapping.RowAction = "D";
									_rowMapping.MapID = rowMapping3.MapID;
									_rowMapping.SortCenter = rowMapping3.SortCenter;
									_rowMapping.ClientName = rowMapping3.ClientName;
									_rowMapping.PostalCode = rowMapping3.PostalCode;
									_rowMapping.CountryID = rowMapping3.CountryID;
									_rowMapping.Country = rowMapping3.Country;
									_rowMapping.StateOrProvince = rowMapping3.StateOrProvince;
									if(!rowMapping3.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = rowMapping3.PathIDTsort;
									if(!rowMapping3.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = rowMapping3.TsortPathMnemonic;
									if(!rowMapping3.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = rowMapping3.ServiceIDTsort;
									if(!rowMapping3.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = rowMapping3.TsortServiceMnemonic;
									if(!rowMapping3.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = rowMapping3.PathIDReturns;
									if(!rowMapping3.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = rowMapping3.ReturnPathMnemonic;
									if(!rowMapping3.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = rowMapping3.ServiceIDReturns;
									if(!rowMapping3.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = rowMapping3.ReturnServiceMnemonic;
									_rowMapping.LastUpdated = DateTime.Now;
									_rowMapping.UserID = Environment.UserName;
									_rowMapping.RowVersion = rowMapping3.RowVersion;
									_rowMapping.Status = "";
									#endregion
									this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
								}
							}
							else {
								rowMapping5 = (Tsort.Enterprise.MapDS.PostalCodeMappingTable5Row)this.m_dsMappingView.PostalCodeMappingTable5.Select("PostalCode='" + sPostalCode + "'")[0];
								if(rowMapping5!=null) {
									_rowMapping = this.mMappingsDS.PostalCodeMappingTable.NewPostalCodeMappingTableRow();
									#region Copy field-by-field
									_rowMapping.RowAction = "D";
									_rowMapping.MapID = rowMapping5.MapID;
									_rowMapping.SortCenter = rowMapping5.SortCenter;
									_rowMapping.ClientName = rowMapping5.ClientName;
									_rowMapping.PostalCode = rowMapping5.PostalCode;
									_rowMapping.CountryID = rowMapping5.CountryID;
									_rowMapping.Country = rowMapping5.Country;
									_rowMapping.StateOrProvince = rowMapping5.StateOrProvince;
									if(!rowMapping5.IsPathIDTsortNull()) _rowMapping.OldPathIDTsort = rowMapping5.PathIDTsort;
									if(!rowMapping5.IsTsortPathMnemonicNull()) _rowMapping.OldTsortPathMnemonic = rowMapping5.TsortPathMnemonic;
									if(!rowMapping5.IsServiceIDTsortNull()) _rowMapping.OldServiceIDTsort = rowMapping5.ServiceIDTsort;
									if(!rowMapping5.IsTsortServiceMnemonicNull()) _rowMapping.OldTsortServiceMnemonic = rowMapping5.TsortServiceMnemonic;
									if(!rowMapping5.IsPathIDReturnsNull()) _rowMapping.OldPathIDReturns = rowMapping5.PathIDReturns;
									if(!rowMapping5.IsReturnPathMnemonicNull()) _rowMapping.OldReturnPathMnemonic = rowMapping5.ReturnPathMnemonic;
									if(!rowMapping5.IsServiceIDReturnsNull()) _rowMapping.OldServiceIDReturns = rowMapping5.ServiceIDReturns;
									if(!rowMapping5.IsReturnServiceMnemonicNull()) _rowMapping.OldReturnServiceMnemonic = rowMapping5.ReturnServiceMnemonic;
									_rowMapping.LastUpdated = DateTime.Now;
									_rowMapping.UserID = Environment.UserName;
									_rowMapping.RowVersion = rowMapping5.RowVersion;
									_rowMapping.Status = "";
									#endregion
									this.mMappingsDS.PostalCodeMappingTable.AddPostalCodeMappingTableRow(_rowMapping);
								}
							}
							this.btnEdit.Enabled = this.btnDel.Enabled = false;
							break;
					}
				}
				else {
					//Select row and notify user
					for(int i=0; i<this.grdOverride.Rows.Count; i++) {
						if(this.grdOverride.Rows[i].Cells["PostalCode"].Value.ToString()==sPostalCode) {
							this.grdOverride.ActiveRow = this.grdOverride.Rows[i];
							this.grdOverride.ActiveRow.Selected = true;
							break;
						}
					}
					MessageBox.Show(this, "Postal code is under edit.");
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
