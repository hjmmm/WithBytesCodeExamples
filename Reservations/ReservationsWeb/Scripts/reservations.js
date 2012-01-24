
$(document).ready(function () {
    $('.datepicker').datepicker({ dateFormat: "mm/dd/yy" });
    $(".message").delay(2000).fadeOut();
    $("#daily_list_date").change(function () {
        var dateParts = /(\d{2})\/(\d{2})\/(\d{4})/.exec(this.value);
        if (dateParts) {
            $.ajax({
                url: $("#daily_list_holder").data("url") + "/" + dateParts[3] + "/" + dateParts[1] + "/" + dateParts[2],
                success: function (data) {
                    if (data.success) {
                        showReservationList(data.list);
                    } else {
                        alert("Invalid date");
                    }
                }
            });
        } else {
            alert("Invalid date");
        }
    });
});


function showReservationList(list) {
    var holder = $("#daily_list_holder");
    var content;
    holder.empty();
    if (list.length == 0) {
        holder.append('<p class="message">No reservations available for the selected date.</p>');
        return;
    }
    content = '<table><thead><tr><th class="description">Customer name</th><th>Number of Guests</th></tr></thead><tbody>';
    for(var i in list){
        content += "<tr><td>" + list[i].name + "</td><td>" + list[i].numberOfGuests + "</td></tr>";
    }
    content += "</tbody></table>"
    holder.append(content);
}