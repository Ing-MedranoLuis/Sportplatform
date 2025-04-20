var dataTable;

$(document).ready(function () {
    loadDataTable()
});


function loadDataTable() {
    dataTable = $('#tblJugador').DataTable({
        "ajax": { url: '/admin/jugador/getall' },
        "columns": [
            { data: 'nombre', "width": "30%" },
            { data: 'equipo.nombre', "width": "15%" },
            { data: 'poscion', "width": "5%" },
            { data: 'cedula', "width": "25%" },

            {
                data: 'id',
                "render": function (data) {
                    return `<did>

<a href="/admin/jugador/upsert?id=${data}" class="btn btn-primary"><i class="bi bi-pencil-fill"></i>Edit</a>
   <a onClick=Delete('/admin/jugador/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}