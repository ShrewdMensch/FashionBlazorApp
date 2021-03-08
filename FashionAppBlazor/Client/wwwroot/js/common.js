window.ShowSwal = (type, message) => {
    swal({
        text: message,
        closeOnClickOutside: false,
        icon: type,
        button: "Ok",

    });
}

function ShowDeleteConfirmationModal() {
    $('#deleteModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteModal').modal('hide');
}