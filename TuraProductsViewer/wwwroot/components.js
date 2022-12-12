function alertUser(text) {
        const toast = swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            padding: '2em'
        });

        toast({
            title: text,
            padding: '2em',
        })
}

function alertUserSuccess(text) {
    const toast = swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        padding: '2em'
    });

    toast({
        type: 'success',
        title: text,
        padding: '2em',
    })
}

function alertUserError(text) {
    const toast = swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        padding: '2em'
    });

    toast({
        type: 'error',
        title: text,
        padding: '2em',
    })
}