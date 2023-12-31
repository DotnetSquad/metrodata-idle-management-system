﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
new DataTable('#data-table', {
    scrollX: true
});

// Show a confirmation dialog before deleting a record - See https://sweetalert2.github.io/
document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-button');
    deleteButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault(); // Prevent the default form submission
            const form = event.target.closest('form');
            const guid = form.querySelector('input[name="guid"]').value;

            Swal.fire({
                title: 'Are you sure?',
                text: 'You won\'t be able to recover this data!',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'No, cancel',
            }).then((result) => {
                if (result.isConfirmed) {
                    // If the user confirms, submit the form
                    const form = event.target.closest('form');
                    form.submit();
                } else {
                    Swal.fire(
                        'Cancelled',
                        'Your data is safe :)',
                        'info'
                    );
                }
            });
        });
    });
});
