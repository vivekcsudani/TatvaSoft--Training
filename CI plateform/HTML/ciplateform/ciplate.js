 window.onload = function () {
    // your code 

    const listViewButton = document.querySelector('#list-view-button');
    const gridViewButton = document.querySelector('#grid-view-button');
    const list = document.getElementsByClassName('bg_style');
    const view = document.getElementsByClassName('card');
    const page = document.getElementsByClassName('page-item');

    listViewButton.onclick = function () {
        for (let i = 0; i < 9; i++) {
            list[i].classList.remove('col-12');
            list[i].classList.add('col-lg-4');
            list[i].classList.add('col-md-6');
            view[i].classList.remove('flex-row');
            view[i].classList.add('flex-column');
            listViewButton.classList.add('active');
            gridViewButton.classList.remove('active');
        }
    }

    gridViewButton.onclick = function () {
        for (let i = 0; i < 9; i++) {
            list[i].classList.remove('col-lg-4');
            list[i].classList.remove('col-md-6');
            list[i].classList.add('col-12');
            view[i].classList.remove('flex-column');
            view[i].classList.add('flex-row');
            gridViewButton.classList.add('active');
            listViewButton.classList.remove('active');
        }
    }
};

