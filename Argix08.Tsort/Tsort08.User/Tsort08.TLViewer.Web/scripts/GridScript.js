var mActiveRowID;
var mActiveRow = null;
var mPrevActiveRow = null;
var mShiftKeyOn = false;
var mCtrlKeyOn = false;
var mMixedSelect = false;
var mMultiSelect = false;
var mMultiSelectTopRowIndex = 0;
var mMultiSelectBottomRowIndex= 0;

function rowSelection() {
    //
	document.all.TLBody.focus();
	var activeCell = window.event.srcElement;
    mActiveRow = window.event.srcElement.parentElement;
	if(mActiveRow.tagName != "TR")
		return;
	mActiveRowID = parseInt(mActiveRow.id);
	if(mShiftKeyOn) {
		if(mPrevActiveRow != null) {
		    //Should NOT be null if already a multiselect, but CTL key can set mPrevActiveRow = null
		    selectMultipleRows(); 
		}
		else {
			if(mMultiSelect) { unselectMultipleRows(); }
			toggleRowSelection();
		}
	}
	else {
		if(mCtrlKeyOn) {
			mMixedSelect = true;
			toggleRowSelection();
		}
		else {
			if(mMultiSelect || mMixedSelect) {
				unselectMultipleRows();
			}
			else {
				if(mPrevActiveRow != null) {
					if(mPrevActiveRow != mActiveRow) unselectPreviousRow();
				}
			}
			toggleRowSelection();
		}
	}
	doCalculation();
}
function toggleRowSelection() {
	if(mActiveRow.className == 'SelectedRow') {
		mActiveRow.className = 'NormalRow';
		mPrevActiveRow = null;
	}
	else {	
		mActiveRow.className = 'SelectedRow';
		mPrevActiveRow = mActiveRow;
	}
}
function unselectAllRows() {
	var i;
	for(i=0; i<document.all.grdTLs.TotalRows - 1; i++) { unselectRow(i); }
	mMixedSelect = false;
}
function unselectRangeOfRows() {
	var i = mMultiSelectTopRowIndex;
	if(mMultiSelectTopRowIndex != mMultiSelectBottomRowIndex) {
		for(; i <= mMultiSelectBottomRowIndex; i++) { unselectRow(i); }
	}
}
function unselectMultipleRows() {
	if(mMixedSelect)
		unselectAllRows();
	else
		unselectRangeOfRows();
	mMultiSelect = false;	
}
function unselectPreviousRow() {
	unselectRow(parseInt(mPrevActiveRow.id));
}
function unselectRow(index) {
	var thisElement = document.getElementById(index.toString() + "row"); 
	if(thisElement != null && thisElement != 'undefined')
		thisElement.className = 'NormalRow'; 
}
function selectRow(index) {
	var thisElement = document.getElementById(index.toString() + "row"); 
	if(thisElement != null && thisElement != 'undefined') 
	    thisElement.className = 'SelectedRow';
	mPrevActiveRow = thisElement;
}
function selectMultipleRows() {
	if(mMultiSelect)  {
	    //there is already a multiple selection on the grid
	    //if current row is not already selected (or within selection range) then extend the selection 
		if (mActiveRowID > mMultiSelectBottomRowIndex || mActiveRowID < mMultiSelectTopRowIndex) {
			extendSelection();
		}
		else {	
		    unselectMultipleRows();
			toggleRowSelection();
		}
	}
	else {
		selectRows();
	}
}
function selectRows() {
	var prevRowID = parseInt(mPrevActiveRow.id);
	var i = mActiveRowID;
	if (mActiveRowID < prevRowID) {
		for (; i<= prevRowID; i++) { selectRow(i); }
		mMultiSelectTopRowIndex = mActiveRowID;
		mMultiSelectBottomRowIndex = prevRowID;
	}
	else {
		for (; i>= prevRowID; i--) { selectRow(i); }
		mMultiSelectTopRowIndex = prevRowID;
		mMultiSelectBottomRowIndex = mActiveRowID;
	}
	mMultiSelect = true;
}
function extendSelection() {
	var i;
	if(mActiveRowID > mMultiSelectBottomRowIndex) {
		i = mMultiSelectBottomRowIndex;
		for (; i <= mActiveRowID; i++) { selectRow(i); }
		mMultiSelectBottomRowIndex = mActiveRowID;
	}
	else {
		if(mActiveRowID < mMultiSelectTopRowIndex) {
			i = mActiveRowID;
			for(; i <= mMultiSelectTopRowIndex; i++) { selectRow(i); }
			mMultiSelectTopRowIndex = mActiveRowID;
		}
	}
	mMultiSelect = true;
}
function keyDownEvent() {
	switch(window.event.keyCode) {
		case 16: mShiftKeyOn = true; break;
		case 17: mCtrlKeyOn = true; break;
	}
}
function keyUpEvent() {
	switch(window.event.keyCode) {
		case 16: mShiftKeyOn = false; break;
		case 17: mCtrlKeyOn = false; break;
	}
}
function keyPressed() {
	if(window.event.shiftKey) {
		switch (event.keyCode) {
			case 38: SelectRowUp(); break;
			case 40: SelectRowDown(); break;
		}
	}
}
function setActiveRow(index) {
	mActiveRow = document.getElementById(index.toString() + "row");
	if(mActiveRow.tagName != "TR")
		return;
	mActiveRowID = parseInt(mActiveRow.id);
	selectRow(mActiveRowID);
	doCalculation();
	mMixedSelect = true;
}
function SelectRowUp() {
	if(mActiveRowID != null && mActiveRowID != 0) {	
		setActiveRow(mActiveRowID - 1);
	}
}
function SelectRowDown() {
	if(mActiveRowID == null) {
		setActiveRow(0);
	}
	else {	
		if(mActiveRowID != document.all.grdTLs.TotalRows - 1) { setActiveRow(mActiveRowID + 1); }
	}
}
function findTL() {
    //
    //if(window.event.keyCode == 13) {
        var grd = document.getElementById('grdTLs');
        for(var i=0; i<grd.rows.length; i++) { unselectRow(i); }
        doCalculation();
        
        var txt = document.getElementById('txtFind').value;
        if(txt.length > 0) {
            for(var i=1; i<grd.rows.length; i++) {
                var cell = grd.rows[i].cells[0];
                if(cell.innerHTML.substr(0, txt.length) == txt) {
                    var pnl = document.getElementById('pnlTLs');
                    pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                    selectRow(i);
                    doCalculation();
                    break; 
                }
            }
        }
    //}
}
