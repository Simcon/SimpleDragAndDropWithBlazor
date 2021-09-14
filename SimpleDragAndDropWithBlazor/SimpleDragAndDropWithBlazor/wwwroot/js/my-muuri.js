window.initMuuri = () => {
    var dragContainer = document.querySelector('.muuri-drag-container');
    var cardContainers = [].slice.call(document.querySelectorAll('.muuri-board-column-content'));
    var columnGrids = [];
    var boardGrid;
    // Init the column grids so we can drag those items around.
    cardContainers.forEach(function (container) {
        var grid = new Muuri(container, {
            items: '.muuri-card',
            dragEnabled: true,
            dragHandle: '.muuri-card-handle',
            dragSort: function () {
                return columnGrids;
            },
            dragContainer: document.body,
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
            .on('dragStart', function (item) {
                console.log('dragStart');
            })
            .on('dragReleaseEnd', function (item) {
                console.log('dragReleaseEnd');
                item.getElement().style.width = '';
                item.getElement().style.height = '';
                item.getGrid().refreshItems([item]);
                doit();
            })
            .on('layoutStart', function (items) {
                console.log('layoutStart');
                boardGrid.refreshItems().layout();
            });

        //grid.synchronize();

        columnGrids.push(grid);

        
        //grid.getItems().forEach(function (item) {
        //    console.log(item.getPosition());
        //    //item.getGrid().refreshItems();
        //    item.getGrid().synchronize();
        //});

        //grid.getItems().layout();

    });

    // Init board grid so we can drag those columns around.
    boardGrid = new Muuri('.muuri-board', {
        dragEnabled: true,
        dragHandle: '.muuri-board-column-header'
    });

    doit = function (order) {
        var columns = columnGrids.map(column => {
            return column.getItems()
                .map(item => { return { id: item.getElement().getAttribute('data-id') } });
        });
        console.log(columns);
        ref.invokeMethodAsync('UpdateLayoutCaller', columns);
    }
};

window.updateMessageCaller = (dotnetHelper) => {
    dotnetHelper.invokeMethodAsync('UpdateMessageCaller');
    dotnetHelper.dispose();
}

window.setref = (ref) => {
    window.ref = ref;
}

window.applyStyleForElement = function (styleOp) {
    document.getElementById(styleOp.id).style[styleOp.attrib] = styleOp.value;
}
