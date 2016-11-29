function CheckAll(me, groupName) {
    for(i=0; i<document.forms[0].length; i++) { 
        var o=document.forms[0][i]; 
        if(o.type == 'checkbox') { 
            if(me.name != o.name)  {
                if(o.id.substr(0, groupName.length) == groupName) {
                    //Must be this way
                    o.checked = !me.checked; 
                    o.click(); 
                }
            }
        } 
    } 
}
function ApplyStyle(me, selectedForeColor, selectedBackColor, foreColor, backColor, bold, checkBoxHeaderId) { 
    var td = me.parentNode; 
    if(td == null) return; 
    var tr = td.parentNode;
    if(me.checked) { 
       tr.style.fontWeight = bold;
       tr.style.color = selectedForeColor; 
       tr.style.backgroundColor = selectedBackColor; 
    } 
    else { 
       var o = document.getElementById(checkBoxHeaderId);
       if(o != null) o.checked = false;
       tr.style.fontWeight = bold; 
       tr.style.color = foreColor; 
       tr.style.backgroundColor = backColor; 
    } 
}
