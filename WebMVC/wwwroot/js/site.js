// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.delete-button').on('click', (ev) => {    
    const id = ev.target.id;
    console.log(id);
    $.post("Trener/Delete/" + id, () => {
        console.log(111);
        location.reload();
    });

})

