$(document).ready(function () {
    $('#btnChangeHtml').click(function () {
        $.get("knas", function (data) {
            $("#pCodeToShow").html(data);
            

        });
        
        var lblUsername = document.getElementById('labelUserName');
        lblUsername.innerHTML = $('#UserName').val();

        $(this).attr('disabled', 'disabled')
    });

    $("#UserName").hover(
            function () {
            document.getElementById('test').style.backgroundColor = 'green';
            }, function () {
            document.getElementById('test').style.backgroundColor = 'white';
     });

});
