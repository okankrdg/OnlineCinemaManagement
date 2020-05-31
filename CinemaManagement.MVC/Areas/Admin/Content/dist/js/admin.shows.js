$(function () {
    $('.datetimepicker1').datetimepicker({
        format: "d/m/Y H:i",
        minDate: 0,
        defaultTime:"00:00",
        timepicker:false
    })
    $(document).on('click', "#Times", function (e) {
        e.preventDefault();
        picker();

    })
    function picker() {
        $('.timepicker').datetimepicker({
            step: 5,
            datepicker: false,
            defaultTime: '12:00',
            format: "H:i"
        });
    }
    $('#timeSpanAdd').click(function (e) {
        e.preventDefault();
        var time = $('.timepicker').last();
        var timeCln = $(time).clone();

        var attributes = $(time).prop("attributes");
        $.each(attributes, function () {
            timeCln.attr(this.name, this.value)
        })
        timeCln.css("margin-top", "10px")
        timeCln.insertAfter(".timepicker:last");

    })
    $('#timeSpanRemove').click(function (e) {
        e.preventDefault();
        var time = $('.timepicker');
        if (time.length < 2) {
            alert("Hop yavaş")
            return;
        }
        time.last().remove();
    })
})