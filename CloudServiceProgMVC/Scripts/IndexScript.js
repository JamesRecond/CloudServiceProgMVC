$(document).ready(function () {
    $('#btnChangeHtml').click(function () {
        //alert("snorre");
        //var element = document.getElementById("pCodeToShow");
        //element.innerHTML = "poo";
        //alert("Get test");
        $.get("knas", function (data) {
            $("#pCodeToShow").html(data);
            console.log(document.getElementById("pCodeToShow"));

        });
        
        var lblUsername = document.getElementById('labelUserName');
        lblUsername.innerHTML = $('#UserName').val();
        console.log(document.getElementById("UserName"));
            
    });
});
