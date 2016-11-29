var TRAILERCUBE = 5293555;
var TRAILERWEIGHT = 42500;

function doCalculation() {
    //Get the grid and column indexes
    var oDoc = window.document.all;
	var grid = window.document.all.grdTLs;
    var cartonsIndex = parseInt(grid.CartonsCol);
    var palletsIndex = parseInt(grid.PalletsCol);
    var weightIndex = parseInt(grid.WeightCol);
    var cubeIndex = parseInt(grid.CubeCol);
	
	//Update cartons, pallets, weight, and cube for selected rows
	var totalSelectedRows=0, totalWeight=0, totalPallets=0, totalCubeFt=0, totalCartons=0;
    for(var i=0; i<grid.childNodes(0).childNodes.length; i++) {
		if(grid.childNodes(0).childNodes(i).className == 'SelectedRow') {
			totalSelectedRows++;
			totalCartons = totalCartons + formatInt(grid.childNodes(0).childNodes(i).childNodes(cartonsIndex).innerText);
			totalPallets = totalPallets + formatInt(grid.childNodes(0).childNodes(i).childNodes(palletsIndex).innerText);
            totalWeight = totalWeight + formatInt(grid.childNodes(0).childNodes(i).childNodes(weightIndex).innerText);
			totalCubeFt = totalCubeFt + formatInt(grid.childNodes(0).childNodes(i).childNodes(cubeIndex).innerText);
		}
	}
	oDoc.TotalTLs.innerText = totalSelectedRows;
	oDoc.TotalWeight.innerText = formatNumber(totalWeight);
	oDoc.TotalCubeFt.innerText = formatNumber(totalCubeFt);
	oDoc.TotalPallets.innerText = formatNumber(totalPallets);
    oDoc.TotalCartons.innerText = formatNumber(totalCartons);
    
	//Update ISA weight/cube for selected rows
	var isaWeight=0, isaCubeFt=0;
    if(isNaN(parseFloat(oDoc.ISAWeight.value)))
		isaWeight = 0;
	else
		isaWeight = parseFloat(oDoc.ISAWeight.value);
	isaCubeFt = calculateISACube(isaWeight);
	oDoc.ISACubeFt.innerText = formatNumber(isaCubeFt);
	
	//Update total weight/cube and weight/cube% for selected rows
    var grandWeight = totalWeight + isaWeight;
	var grandCubeFt = totalCubeFt + isaCubeFt;
	oDoc.GrandWeight.innerText = formatNumber(grandWeight);
	oDoc.GrandCubeFt.innerText = formatNumber(grandCubeFt);
	oDoc.WeightPercent.innerText = parseInt(grandWeight * 100 / TRAILERWEIGHT) + "%";
	oDoc.CubePercent.innerText = parseInt(grandCubeFt * 100 / TRAILERCUBE) + "%";
}
function calculateISACube(isaWeight) { return parseInt(isaWeight * TRAILERCUBE / TRAILERWEIGHT); }
function formatNumber(numString) {
	numString = numString.toLocaleString();
	var exp = /\./;
	var decimalPos = numString.search(exp); //will search for decimal
	if (decimalPos != -1 && decimalPos > 0)
		return numString.substr(0,decimalPos);
	else
		return numString;
}
function formatInt(numString) { return parseInt(numString.replace(/\$|\,/g,'')); }