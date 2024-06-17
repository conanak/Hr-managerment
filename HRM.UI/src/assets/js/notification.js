$(function () {
	$.ajax({
		url: "http://localhost:5097/api/notification/getall",
		method: "GET",
		contentType: "application/json",
		success: function (response) {
			$("#notification-dropdown").empty();

			let notificationHtml = `<a class="nav-link count-indicator" id="notificationDropdown" href="#" data-bs-toggle="dropdown">
                                        <i class="icon-bell"></i>
                                        <span class="count"></span>
                                    </a>
                                    <div class="dropdown-menu dropdown-menu-right navbar-dropdown preview-list pb-0"
                                        aria-labelledby="notificationDropdown">
                                        <a class="dropdown-item py-3 border-bottom" id="notification-total-count">
                                        <p class="mb-0 fw-medium float-start">You have ${response.totalCount} new notifications </p>
                                        </a>`;

			notificationHtml += response.datas
				.map((notification) => {
					return `
                    <a class="dropdown-item preview-item py-3">
                        <div class="preview-item-content">
                            <h6 class="preview-subject fw-normal text-dark mb-1">${notification.content}</h6>
                            <p class="fw-light small-text mb-0">${notification.publishedTime}</p>
                        </div>
                    </a>`;
				})
				.join("");

			notificationHtml += `</div>`;

			$("#notification-dropdown").append(notificationHtml);
		},
		error: function (xhr) {
			console.error(xhr.responseText);
		},
	});
});
