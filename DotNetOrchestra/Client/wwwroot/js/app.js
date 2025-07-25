window.noteDragHelper = {
    dotNetRef: null,
    moveHandler: null,
    upHandler: null,

    attachListeners: function (dotNetRef) {
        this.dotNetRef = dotNetRef;

        this.moveHandler = (e) => {
            this.dotNetRef.invokeMethodAsync('OnMouseMove', e.clientX, e.clientY);
        };
        this.upHandler = () => {
            this.dotNetRef.invokeMethodAsync('OnMouseUp');
        };

        document.addEventListener('mousemove', this.moveHandler);
        document.addEventListener('mouseup', this.upHandler);
    },

    detachListeners: function () {
        document.removeEventListener('mousemove', this.moveHandler);
        document.removeEventListener('mouseup', this.upHandler);

        this.dotNetRef = null;
        this.moveHandler = null;
        this.upHandler = null;
    },

    getWindowSize: function () {
        return {
            width: window.innerWidth,
            height: window.innerHeight
        };
    }
};

async function getSvg(name) {
    var path = `svg/${name}.svg`;
    var response = await fetch(path);

    if (response.status !== 200)
        throw `Запрашиваемый файл не найден (${path})`;

    return response.text();
}