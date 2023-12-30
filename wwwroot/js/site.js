// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//Handle active page link
let links = document.getElementsByTagName("a");

for (var i = 1; i < links.length; i++) {
    if (links[i].href == document.URL) {
        links[i].classList.add("active-link");
        links[i].previousElementSibling.classList.add("active-link");
    }
}

//Handle modal popup
$(function () {
    $('button[data-toggle="ajax-modal-checkin"]').click(function (event) {
        event.preventDefault();

        let position = $('.form-select').val(); //Get value of select

        $.ajax({
            url: '/Home/CheckIn', //URL Action
            type: 'POST',
            data: { position: position }, //Send value of select
            success: function (response) {
                //Handle response from server here
                $('#PlaceHolderHere').html(response);
                $('#PlaceHolderHere').find('.modal').modal('show');
            }
        });
    });
});

$(function () {
    $('button[data-toggle="ajax-modal-checkout"]').click(function (event) {
        event.preventDefault();

        $.ajax({
            url: '/Home/CheckOut', //URL Action
            type: 'POST',
            success: function (response) {
                //Handle response from server here
                $('#PlaceHolderHere').html(response);
                $('#PlaceHolderHere').find('.modal').modal('show');
            }
        });
    });
});

//Handle modal popup
$(function () {
    $('button[data-toggle="ajax-modal-user-detail"]').click(function (event) {
        event.preventDefault();

        $.ajax({
            url: '/Profile/Detail', //URL Action
            type: 'POST',
            success: function (response) {
                //Handle response from server here
                $('#UserDetailPlaceHolder').html(response);
                $('#UserDetailPlaceHolder').find('.modal').modal('show');
            }
        });
    });
});