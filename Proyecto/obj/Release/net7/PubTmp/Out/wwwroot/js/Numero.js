var dataTable;

$(document).ready(function () {
    loadDataTable()
});


function loadDataTable() {
    dataTable = $('#tblNumero').DataTable({
        "ajax": { url: '/admin/numero/getall' },
        "columns": [
            { data: 'jugador.nombre', "width": "10%" },
            { data: 'h1', "width": "10%" },
            { data: 'h2', "width": "10%" },
            { data: 'h3', "width": "10%" },
            { data: 'h4', "width": "10%" },
            { data: 'bb', "width": "10%" },
            { data: 'kk', "width": "5%" },
            { data: 'avg', "width": "5%" },
           

            {
                data: 'id',
                "render": function (data) {
                    return `<did>

<a href="/admin/numero/upsert?id=${data}" class="btn btn-primary"><i class="bi bi-pencil-fill"></i></a>
   <a onClick=Delete('/admin/numero/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> </a>
                    </div>`
                },
                "width": "20%"
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