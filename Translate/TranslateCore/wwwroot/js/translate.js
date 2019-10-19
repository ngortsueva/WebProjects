function translate(event) {
    if (event.which != 13) return;

    var wordeng = document.getElementById("wordeng").value;

    if (wordeng != "") {
        var urlTranslate = `/Translate/Translate?wordeng=${wordeng}`;
        $.ajax({
            url: urlTranslate,
            success: function (data) {
                if (data != null) {
                    var word = JSON.parse(data);

                    var textWordru = document.getElementById("wordru");

                    textWordru.innerHTML = word[0].WordRu;
                }
            }
        });
    }
}

function filterBySymbol(event) {
    var select = event.target.innerHTML;

    var urlTranslate = `/Dictionary/Index?filter=${select}`;
    $.ajax({
        url: urlTranslate,
        success: function (data) {
            if (data != null) {
                document.open();
                document.write(data);                
                document.close();
            }
        }
    });
}

function filterWord(event, controller) {
    var select = event.target.innerHTML;

    var urlTranslate = `/${controller}/Index?filter=${select}`;
    $.ajax({
        url: urlTranslate,
        success: function (data) {
            if (data != null) {
                document.open();
                document.write(data);
                document.close();
            }
        }
    });
}

function checkIsExist(event) {
    var inputText = document.getElementById('WordEng').value;

    var label = document.getElementById('labelExist');
    var buttonCreate = document.getElementById('buttonCreate');

    label.style.display = "hidden";

    if (inputText.length > 2) {
        var urlCheck = `/Dictionary/IsExist?word=${inputText}`;
        $.ajax({
            url: urlCheck,
            success: function (data) {
                if (data != null && data.length != 0) {
                    label.style.visibility = "visible";
                    buttonCreate.disabled = true;
                }
                else {
                    label.style.visibility = "hidden";
                    buttonCreate.disabled = false;
                }
            }
        });
    }    
}

function showModal(form, wordId) {
    var urlGet = '';

    var divType = document.getElementById(`${form}`);

    switch (form) {
        case 'AddWord': urlGet = `/Dictionary/${form}`; break;
        case 'EditWord': urlGet = `/Dictionary/${form}?id=${wordId}`; break;

        default: urlGet = `/Dictionary/${form}?wordId=${wordId}`; break;
    }

    if (divType.innerHTML == "") {
        $.ajax({
            url: urlGet,
            success: function (data) {
                if (data != null && data.length != 0) {
                    var divType = document.getElementById(`${form}`);

                    divType.innerHTML = data;

                    $(`#modal${form}`).modal();
                }
            }
        });
    }
    else {
        document.getElementById('hiddenWordId').value = wordId;
    }
    $(`#modal${form}`).modal();
}

function addType(typeId) {

    var wordId = document.getElementById('hiddenWordId').value;

    $.ajax({
        url: `/Dictionary/AddType`,
        type: "POST",
        data: { word: wordId, type: typeId },
        success: function (data) {
            if (data != null && data.length != 0) {                

                $(`#modalAddType`).modal('hide');

                if (data != "") {
                    var res = JSON.parse(data);
                    var tbl = document.getElementById("dictionary");

                    var rowIndex = document.getElementById(res.wordId).rowIndex;

                    tbl.rows[rowIndex].cells[3].innerHTML = res.typeName;

                    //col.appendChild("text");
                }
            }
        }
    });
}

function addCategory(categoryId) {

    var wordId = document.getElementById('hiddenWordId').value;

    $.ajax({
        url: `/Dictionary/AddCategory`,
        type: "POST",
        data: { word: wordId, category: categoryId },
        success: function (data) {
            if (data != null && data.length != 0) {

                $(`#modalAddCategory`).modal('hide');

                if (data != "") {
                    var res = JSON.parse(data);
                    var tbl = document.getElementById("dictionary");

                    var rowIndex = document.getElementById(res.wordId).rowIndex;

                    tbl.rows[rowIndex].cells[4].innerHTML = res.catName;                    
                }
            }
        }
    });
}