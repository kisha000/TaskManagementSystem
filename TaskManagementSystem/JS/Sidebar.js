const sidebar = document.getElementById('sidebar');
const collapseBtn = document.getElementById('collapseBtn');
const content = document.getElementById('content');

collapseBtn.addEventListener('click', function () {
    sidebar.classList.toggle('collapsed');
    content.classList.toggle('expanded');
    if (sidebar.classList.contains('collapsed')) {
        collapseBtn.innerHTML = '&#x2192;';
    } else {
        collapseBtn.innerHTML = '&#x2190;';
    }
});