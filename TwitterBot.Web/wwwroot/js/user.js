function Delete(id) {
    Swal.fire({
        title: 'Are You Sure',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        CancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/Users/Delete',
                type: 'DELETE',
                data: { id: id },
                success: function (data) {
                    if (data.success) {
                        document.location.reload();
                        toaster.success(data.message);
                    }
                    else {
                        toaster.error(data.message);
                    }
                }
            })
        }
    }
    )
}