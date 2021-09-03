﻿window.initMuuri = () => {
    console.log('initMuuri');

    var dragContainer = document.querySelector('.muuri-drag-container');
    var itemContainers = [].slice.call(document.querySelectorAll('.muuri-board-column-content'));
    var columnGrids = [];
    var boardGrid;

    // Init the column grids so we can drag those items around.
    itemContainers.forEach(function (container) {
        var grid = new Muuri(container, {
            items: '.muuri-board-item',
            dragEnabled: true,
            dragSort: function () {
                return columnGrids;
            },
            dragContainer: dragContainer,
            dragAutoScroll: {
                targets: (item) => {
                    return [
                        { element: window, priority: 0 },
                        { element: item.getGrid().getElement().parentNode, priority: 1 },
                    ];
                }
            },
        })
            .on('dragInit', function (item) {
                console.log('dragInit');
                item.getElement().style.width = item.getWidth() + 'px';
                item.getElement().style.height = item.getHeight() + 'px';
            })
            .on('dragReleaseEnd', function (item) {
                console.log('dragReleaseEnd');
                item.getElement().style.width = '';
                item.getElement().style.height = '';
                item.getGrid().refreshItems([item]);
            })
            .on('layoutStart', function () {
                console.log('layoutStart');
                boardGrid.refreshItems().layout();
            });

        columnGrids.push(grid);
    });

    // Init board grid so we can drag those columns around.
    boardGrid = new Muuri('.muuri-board', {
        dragEnabled: true,
        dragHandle: '.muuri-board-column-header'
    });
};