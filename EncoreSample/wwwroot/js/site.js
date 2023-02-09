// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const placeholder = $("#modal-content");

function modal(id, details) {
    const url = id ? (details ? `/Audios/Details/${id}` : `/Audios/Edit/${id}`) : '/Audios/CreateModal';
    console.log(url);
    $.get(url, function (res) {
        placeholder.html(res);
    })

    $("#inputModal").modal('show');

}

function deleteModal(id) {
    $.get(`/Audio/Delete/${id}`, function (res) {
        placeholder.html(res);
    })
    $("#inputModeal").modal('show');
}