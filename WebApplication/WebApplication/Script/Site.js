$("[type=reset]").click(function (e) {
    e.preventDefault();
    location = location;
});

$("[data-url]").click(function (e) {
    e.preventDefault();
    var url = $(this).data("url");
    location = url;
});

$("[data-confirm]").click(function (e) {
    var message = $(this).data("confirm");
    var result = confirm(message);
    if (result == false) {
        e.preventDefault();
    }
});

$("[data-upper]").on("input", function (e) {
    var start = this.selectionStart;
    var end = this.selectionEnd;

    var text = $(this).val();
    $(this).val(text.toUpperCase());

    this.setSelectionRange(start, end);
});