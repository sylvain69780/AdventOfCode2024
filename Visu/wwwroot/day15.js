// https://webglfundamentals.org/webgl/lessons/webgl-fundamentals.html

window.day15 = {
    initializeScene: (canvasId) => {

        function createShader(gl, type, source) {
            var shader = gl.createShader(type);
            gl.shaderSource(shader, source);
            gl.compileShader(shader);
            var success = gl.getShaderParameter(shader, gl.COMPILE_STATUS);
            if (success) {
                return shader;
            }

            console.log(gl.getShaderInfoLog(shader));
            gl.deleteShader(shader);
        }
        function createProgram(gl, vertexShader, fragmentShader) {
            var program = gl.createProgram();
            gl.attachShader(program, vertexShader);
            gl.attachShader(program, fragmentShader);
            gl.linkProgram(program);
            var success = gl.getProgramParameter(program, gl.LINK_STATUS);
            if (success) {
                return program;
            }

            console.log(gl.getProgramInfoLog(program));
            gl.deleteProgram(program);
        }

        function resizeCanvasToDisplaySize(canvas) {
            // Lookup the size the browser is displaying the canvas in CSS pixels.
            const displayWidth = canvas.clientWidth;
            const displayHeight = canvas.clientHeight;

            // Check if the canvas is not the same size.
            const needResize = canvas.width !== displayWidth ||
                canvas.height !== displayHeight;

            if (needResize) {
                // Make the canvas the same size
                canvas.width = displayWidth;
                canvas.height = displayHeight;
            }
            return needResize;
        }

        const canvas = document.getElementById(canvasId);
        var gl = canvas.getContext("webgl");
        if (!gl)
            return;
        resizeCanvasToDisplaySize(gl.canvas);
        var vertexShaderSource = document.querySelector("#vertex-shader-2d").text;
        var fragmentShaderSource = document.querySelector("#fragment-shader-2d").text;

        var vertexShader = createShader(gl, gl.VERTEX_SHADER, vertexShaderSource);
        var fragmentShader = createShader(gl, gl.FRAGMENT_SHADER, fragmentShaderSource);
        var program = createProgram(gl, vertexShader, fragmentShader);
        var positionAttributeLocation = gl.getAttribLocation(program, "a_position");
        var positionBuffer = gl.createBuffer();
        gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);
        var resolutionUniformLocation = gl.getUniformLocation(program, "u_resolution");

        var positions = [
            0, 0,
            gl.canvas.width - 1,0,
            gl.canvas.width-1, gl.canvas.height-1,
            gl.canvas.width - 1, gl.canvas.height - 1,
            0, gl.canvas.height - 1,
            0, 0
        ];
        gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(positions), gl.STATIC_DRAW);
        gl.viewport(0, 0, gl.canvas.width, gl.canvas.height);
        // Clear the canvas
        gl.clearColor(0, 0, 0, 0);
        gl.clear(gl.COLOR_BUFFER_BIT);

        gl.useProgram(program);
        gl.enableVertexAttribArray(positionAttributeLocation);
        // Bind the position buffer.
        gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer);

        // Tell the attribute how to get data out of positionBuffer (ARRAY_BUFFER)
        var size = 2;          // 2 components per iteration
        var type = gl.FLOAT;   // the data is 32bit floats
        var normalize = false; // don't normalize the data
        var stride = 0;        // 0 = move forward size * sizeof(type) each iteration to get the next position
        var offset = 0;        // start at the beginning of the buffer
        gl.vertexAttribPointer(
            positionAttributeLocation, size, type, normalize, stride, offset);
        gl.uniform2f(resolutionUniformLocation, gl.canvas.width, gl.canvas.height);

        var primitiveType = gl.TRIANGLES;
        var offset = 0;
        var count = 6;
        gl.drawArrays(primitiveType, offset, count);

        function animate() {
            requestAnimationFrame(animate);
        }
        animate();
    }
};
