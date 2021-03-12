window.ShowSwal = (type, message) => {
    swal({
        text: message,
        closeOnClickOutside: false,
        icon: type,
        button: "Ok",

    });
}

window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, "Operation Successful", { timeOut: 1000 });
    }
    if (type === "error") {
        toastr.error(message, "Operation Failed", { timeOut: 1000 });
    }
}

function ShowDeleteConfirmationModal() {
    $('#deleteModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteModal').modal('hide');
}