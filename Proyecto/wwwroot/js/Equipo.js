var dataTable;

$(document).ready(function () {
    loadDataTable()
});


function loadDataTable() {
    dataTable = $('#tblEquipos').DataTable({
        "ajax": { url: '/admin/equipo/getall' },
        "columns": [
            { data: 'nombre', "width": "50%" },
            { data: 'orden', "width": "25%" },

            {
                data: 'id',
                "render": function (data) {
                    return `<did>

<a href="/admin/equipo/upsert?id=${data}" ><i class="bi bi-pencil-fill"></i></a>
   <a onClick=Delete('/admin/equipo/delete/${data}') > <i class="bi bi-trash-fill"></i> </a>
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