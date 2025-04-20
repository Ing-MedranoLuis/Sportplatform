var dataTable;

$(document).ready(function () {
    loadDataTable()
});


function loadDataTable() {
    dataTable = $('#tblApplicationUser').DataTable({
        "ajax": { url: '/admin/ApplicationUser/getall' },
        "columns": [
            { data: 'nombre', "width": "40%" },
            { data: 'userName', "width": "25%" },
            { data: 'role', "width": "10%" },

            {
                data: { id: "id", lockoutEnd:"lockoutEnd"},
                "render": function (data) {
                    var time = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > time) {
                        return `<did>

<a onclick=LockUnLock('${data.id}') class="btn btn-success"><i class="bi bi-unlock-fill"></i>Unlock</a>
 
                    </div>`
                    } else {
                        return `<did>

<a onclick=LockUnLock('${data.id}') class="btn btn-danger"><i class="bi bi-lock-fill"></i>Lock</a>
 
                    </div>`
                    }
               
                },
                "width": "25%"
            }
        ]
    });
}
function LockUnLock(id){
    console.log(id);
    $.ajax({

        type: "POST",
        url: '/admin/ApplicationUser/LockUnLock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
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