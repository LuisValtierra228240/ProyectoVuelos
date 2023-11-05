function eliminar(id) {
    Swal.fire({
        title: '¿Está seguro que desea eliminar el registro?',
        text: "¡Se eliminará permanentemente!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {

            location.href = "/Vuelo/Eliminar/" + id;

        }
    })
}