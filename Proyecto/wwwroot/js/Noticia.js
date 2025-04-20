var dataTable;

$(document).ready(function () {
    loadDataTable()
});


function loadDataTable() {
    dataTable = $('#tblNoticia').DataTable({
        "ajax": { url: '/admin/noticia/getall' },
        "columns": [
            { data: 'titulo', "width": "25%" },
            { data: 'descripcion', "width": "25%" },
            { data: 'fecha', "width": "25%" },

            {
                data: 'id',
                "render": function (data) {
                    return `<did>

<a href="/admin/noticia/upsert?id=${data}" class="btn btn-primary"><i class="bi bi-pencil-fill"></i></a>
   <a onClick=Delete('/admin/noticia/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> </a>
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