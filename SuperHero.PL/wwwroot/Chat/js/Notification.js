
$(function () {
    var UserId = $("#btn_id").attr("aria-label");
    $.ajax({
        url: '/Notififcation/GetNotiy',
        type: 'GET',
        data: { UserId: UserId }
    }).done(function (server_data) {
        var Notifications = ``;
        $("#NumberCount").html(server_data.Count).css({ "color": "red" });

        $.each(server_data.Notiy, function (i, e) {
            
            Notifications += ` <li class="small mb-0 list-group-item">${e.Notification}</li>`
                ;


           
        })
        $("#Notifcation").html(Notifications);
       
    }).fail(function (jqXHR, status, err) {
        
    });
    function Noty () {
         UserId = $("#btn_id").attr("aria-label");
        $.ajax({
            url: '/Notififcation/GetNotiy',
            type: 'GET',
            data: { UserId: UserId }
        }).done(function (server_data) {
            var Notifications = ``;
            $("#NumberCount").html(server_data.Count);

            $.each(server_data.Notiy, function (i, e) {
              
                Notifications += ` <li class="small mb-0 list-group-item">${e.Notification}</li>`
                    ;


               
            })
            $("#Notifcation").html(Notifications);
          
        }).fail(function (jqXHR, status, err) {
            
        });
    }
    window.setInterval(Noty, 300);
    $('.mm').on("click", function () {
        var UserId = $("#btn_id").attr("aria-label");
        $.ajax({
            url: '/Notififcation/ReadNotiy',
            type: 'GET',
            data: { UserId: UserId }
        }).done(function (server_data) {

            
        }).fail(function (jqXHR, status, err) {
           
        });
    })
});