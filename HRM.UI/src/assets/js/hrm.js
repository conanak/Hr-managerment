$(async () => {
	let totalEarning = await getTotalEarning(0);
	let totalEarningChart = await renderTotalEarningChart(totalEarning);

	let totalVacation = await getTotalVacation(0);
	let totalVacationChart = await renderTotalVacationChart(totalVacation);

	$("#total-earning-shareholder-filter").on("click", async () => {
		$("#total-earning-filter").empty();
		$("#total-earning-filter").append("Shareholder");
		totalEarning = await getTotalEarning(0);
		totalEarningChart.destroy();
		totalEarningChart = await renderTotalEarningChart(totalEarning);
	});

	$("#total-earning-gender-filter").on("click", async () => {
		$("#total-earning-filter").empty();
		$("#total-earning-filter").append("Gender");
		totalEarning = await getTotalEarning(1);
		totalEarningChart.destroy();
		totalEarningChart = await renderTotalEarningChart(totalEarning);
	});

	$("#total-earning-ethnicity-filter").on("click", async () => {
		$("#total-earning-filter").empty();
		$("#total-earning-filter").append("Ethnicity");
		totalEarning = await getTotalEarning(2);
		totalEarningChart.destroy();
		totalEarningChart = await renderTotalEarningChart(totalEarning);
	});

	$("#total-vacation-shareholder-filter").on("click", async () => {
		$("#total-vacation-filter").empty();
		$("#total-vacation-filter").append("Shareholder");
		totalVacation = await getTotalVacation(0);
		totalVacationChart.destroy();
		totalVacationChart = await renderTotalVacationChart(totalVacation);
	});

	$("#total-vacation-gender-filter").on("click", async () => {
		$("#total-vacation-filter").empty();
		$("#total-vacation-filter").append("Gender");
		totalVacation = await getTotalVacation(1);
		totalVacationChart.destroy();
		totalVacationChart = await renderTotalVacationChart(totalVacation);
	});

	$("#total-vacation-ethnicity-filter").on("click", async () => {
		$("#total-vacation-filter").empty();
		$("#total-vacation-filter").append("Ethnicity");
		totalVacation = await getTotalVacation(2);
		totalVacationChart.destroy();
		totalVacationChart = await renderTotalVacationChart(totalVacation);
	});
});

async function getTotalEarning(filterType) {
	let totalEarning;

	await $.ajax({
		url: `http://localhost:5247/api/Employees/get-total-earning/${filterType}`,
		method: "GET",
		contentType: "application/json",
		success: function (response) {
			totalEarning = response;
		},
	});

	return totalEarning;
}

async function getTotalVacation(filterType) {
	let totalVacation;

	await $.ajax({
		url: `http://localhost:5097/api/Vacation/get-total-vacation-days-taken/${filterType}`,
		method: "GET",
		contentType: "application/json",
		success: function (response) {
			totalVacation = response;
		},
	});

	return totalVacation;
}

async function renderTotalEarningChart(totalEarning) {
	let labels = [];
	let toDateValues = [];
	let prevYearValues = [];
	totalEarning.forEach((element) => {
		labels = [...labels, element.name];
		toDateValues = [...toDateValues, element.toDateValue];
		prevYearValues = [...prevYearValues, element.prevYearValue];
	});

	const totalEarningCanvas = document.getElementById("totalEarning");
	let myChart = new Chart(totalEarningCanvas, {
		type: "bar",
		data: {
			labels: labels,
			datasets: [
				{
					label: "To Date",
					data: toDateValues,
					backgroundColor: "#52CDFF",
					borderColor: ["#52CDFF"],
					borderWidth: 0,
					barPercentage: 0.35,
					fill: true,
				},
				{
					label: "Previous Year",
					data: prevYearValues,
					backgroundColor: "#1F3BB3",
					borderColor: ["#1F3BB3"],
					borderWidth: 0,
					barPercentage: 0.35,
					fill: true,
				},
			],
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			elements: {
				line: {
					tension: 0.4,
				},
			},

			scales: {
				y: {
					border: {
						display: false,
					},
					grid: {
						display: true,
						drawTicks: false,
						color: "#F0F0F0",
						zeroLineColor: "#F0F0F0",
					},
					ticks: {
						beginAtZero: false,
						autoSkip: true,
						maxTicksLimit: 4,
						color: "#6B778C",
						font: {
							size: 10,
						},
					},
				},
				x: {
					border: {
						display: false,
					},
					stacked: true,
					grid: {
						display: false,
						drawTicks: false,
					},
					ticks: {
						beginAtZero: false,
						autoSkip: true,
						maxTicksLimit: 7,
						color: "#6B778C",
						font: {
							size: 10,
						},
					},
				},
			},
			plugins: {
				legend: {
					display: true,
				},
			},
		},
	});
	return myChart;
}

async function renderTotalVacationChart(totalVacation) {
	let labels = [];
	let toDateValues = [];
	let prevYearValues = [];
	totalVacation.forEach((element) => {
		labels = [...labels, element.name];
		toDateValues = [...toDateValues, element.toDateValue];
		prevYearValues = [...prevYearValues, element.prevYearValue];
	});

	const totalVacationCanvas = document.getElementById("totalVacation");
	let myChart = new Chart(totalVacationCanvas, {
		type: "bar",
		data: {
			labels: labels,
			datasets: [
				{
					label: "To Date",
					data: toDateValues,
					backgroundColor: "#52CDFF",
					borderColor: ["#52CDFF"],
					borderWidth: 0,
					barPercentage: 0.35,
					fill: true,
				},
				{
					label: "Previous Year",
					data: prevYearValues,
					backgroundColor: "#1F3BB3",
					borderColor: ["#1F3BB3"],
					borderWidth: 0,
					barPercentage: 0.35,
					fill: true,
				},
			],
		},
		options: {
			responsive: true,
			maintainAspectRatio: false,
			elements: {
				line: {
					tension: 0.4,
				},
			},

			scales: {
				y: {
					border: {
						display: false,
					},
					grid: {
						display: true,
						drawTicks: false,
						color: "#F0F0F0",
						zeroLineColor: "#F0F0F0",
					},
					ticks: {
						beginAtZero: false,
						autoSkip: true,
						maxTicksLimit: 4,
						color: "#6B778C",
						font: {
							size: 10,
						},
					},
				},
				x: {
					border: {
						display: false,
					},
					stacked: true,
					grid: {
						display: false,
						drawTicks: false,
					},
					ticks: {
						beginAtZero: false,
						autoSkip: true,
						maxTicksLimit: 7,
						color: "#6B778C",
						font: {
							size: 10,
						},
					},
				},
			},
			plugins: {
				legend: {
					display: true,
				},
			},
		},
	});
	return myChart;
}
