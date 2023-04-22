$(document).ready(function () {
    $(document).on("click", '.open-book-model', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");

        fetch(url)
            .then(response => response.text())
            .then(modalHtml => {
                $("#quickModal .modal-dialog").html(modalHtml)
            });

        $("#quickModal").modal("show")
    })

    $(document).on("click", '.add-basket', function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        let basketNumber = $('.text-number')[0]
        fetch(url)
            .then(response => response.text())
            .then(html => $("#basket-cart").html(html))
    })
})