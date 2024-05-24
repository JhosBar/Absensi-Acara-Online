function SetDateVal(id, date) {
	$(id).val(date);
	if (date) {
		var dd = moment(date, "YYYY MM DD").format("DD/MM/YYYY");
		$(id).data('daterangepicker').setStartDate(dd);
		$(id).data('daterangepicker').setEndDate(dd);
	}
}

function SetTimeVal(id, time) {
	//$(id).val(time);
	if (time) {
		var tt = moment(time, "HH mm").format("HH:mm");
		$(id).flatpickr({
			enableTime: true,
			noCalendar: true,
			dateFormat: "H:i",
			defaultDate: time,
		})
	}
}
$(function () {

	//var start = moment().subtract(29, "days");
	var start = moment().subtract(29, "days");
	var end = moment();

	function cb(start, end) {
		$("#date_range").html(start.format("MMMM D,YYYY") + " - " + end.format("MMMM D,YYYY"));
	}

	$("#date_range").daterangepicker({
		startDate: start,
		endDate: end,
		//maxDate: moment(),
		ranges: {
			"Today": [moment(), moment()],
			"Yesterday": [moment().subtract(1, "days"), moment().subtract(1, "days")],
			"Last 7 Days": [moment().subtract(6, "days"), moment()],
			"Last 30 Days": [moment().subtract(29, "days"), moment()],
			"This Month": [moment().startOf("month"), moment().endOf("month")],
			"Last Month": [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")]
		},
		"locale": {
			"format": "DD/MM/YYYY",
		},
	}, cb);
	cb(start, end);

	$("#date_range").on("apply.daterangepicker", function (ev, picker) {
		var selectedStartDate = picker.startDate;
		var selectedEndDate = picker.endDate;

		// Calculate the difference in days
		var daysDiff = selectedEndDate.diff(selectedStartDate, 'days');

		cb(selectedStartDate, selectedEndDate);
	});

	let currentPage = window.location.pathname;
	$('.menu-link').each(function () {
		let href = $(this).attr('href');
		if (currentPage == href) {
			$(this).addClass('active');
			$(this).parent('.menu-item')
				.parent('.menu-sub')
				.parent('.menu-item').addClass('show')
				.parent('.menu-sub')
				.parent('.menu-item').addClass('show');
			$(this).attr('href', '#')
		}
	});

});