// notification.js
document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        var notification = document.getElementById('notification-message');
        if (notification) {
            notification.classList.add('hidden');  // Añade la clase 'hidden' para el desvanecimiento
        }
    }, 2000);  // 1000 milisegundos = 1 segundo
});
