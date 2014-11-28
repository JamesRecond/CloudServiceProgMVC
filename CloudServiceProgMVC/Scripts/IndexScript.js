$(document).ready(function () {
    $('#btnChangeHtml').click(function () {
        //alert("snorre");
        //var element = document.getElementById("pCodeToShow");
        //element.innerHTML = "poo";
        //alert("Get test");
        $.get("knas", function (data) {
            $("#pCodeToShow").html(data);
            

        });
        
        var lblUsername = document.getElementById('labelUserName');
        lblUsername.innerHTML = $('#UserName').val();
            
    });

    $('#UserName').hover(function() {
        console.log(document.getElementById('knas'));
    });

});
