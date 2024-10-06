// Draggable feature
let commentBox = document.getElementById("floatingBox");
let dragHandle = document.getElementById("dragHandle");
let isDragging = false;
let offsetX, offsetY;

dragHandle.addEventListener("mousedown", (e) => {
    isDragging = true;
    offsetX = e.clientX - commentBox.getBoundingClientRect().left;
    offsetY = e.clientY - commentBox.getBoundingClientRect().top;
    document.addEventListener("mousemove", drag);
    document.addEventListener("mouseup", stopDrag);
});

function drag(e) {
    if (isDragging) {
        commentBox.style.left = e.clientX - offsetX + "px";
        commentBox.style.top = e.clientY - offsetY + "px";
        commentBox.style.right = "auto"; // Prevent right-fixed issue
        commentBox.style.bottom = "auto"; // Prevent bottom-fixed issue
    }
}

function stopDrag() {
    isDragging = false;
    document.removeEventListener("mousemove", drag);
    document.removeEventListener("mouseup", stopDrag);
}

// Resizable feature
let resizeHandle = document.getElementById("resizeHandle");
let isResizing = false;

resizeHandle.addEventListener("mousedown", (e) => {
    isResizing = true;
    document.addEventListener("mousemove", resize);
    document.addEventListener("mouseup", stopResize);
});

function resize(e) {
    if (isResizing) {
        let newWidth = e.clientX - commentBox.getBoundingClientRect().left;
        let newHeight = e.clientY - commentBox.getBoundingClientRect().top;

        // Set new width and height with min/max limits
        if (newWidth > 200 && newWidth < 600) {
            commentBox.style.width = newWidth + "px";
        }
        if (newHeight > 150 && newHeight < 500) {
            commentBox.style.height = newHeight + "px";
        }
    }
}

function stopResize() {
    isResizing = false;
    document.removeEventListener("mousemove", resize);
    document.removeEventListener("mouseup", stopResize);
}