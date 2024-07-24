
console.log(1);
$.ajaxSetup({
    beforeSend: function(xhr) {
        console.log('beforeSend')
        xhr.setRequestHeader("token", localStorage.getItem('access_token'));
    },
    error: function(xhr, status, error) {
        if (xhr.status == 403) {
            window.location.href ="2.html"
        }
    }
})