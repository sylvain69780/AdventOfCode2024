window.day15 = {
    initializeScene: (canvasId) => {
        const canvas = document.getElementById(canvasId);
        const width = canvas.clientWidth;
        const height = canvas.clientHeight;
        const renderer = new THREE.WebGLRenderer({ canvas });
        renderer.setSize(width, height, false);

        const scene = new THREE.Scene();
        scene.background = new THREE.Color(0xffffff);
        const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
        camera.position.z = 3;

        const geometry = new THREE.BoxGeometry();
        const material = new THREE.MeshStandardMaterial({ color: 0x00ff00 });
        const cube = new THREE.Mesh(geometry, material);
        scene.add(cube);
        // Add directional light (like sunlight)
        const directionalLight = new THREE.DirectionalLight(0xffffff, 1);
        directionalLight.position.set(5, 5, 5); // x, y, z
        scene.add(directionalLight);
        function animate() {
            requestAnimationFrame(animate);
            var value = 0.01;
            DotNet.invokeMethodAsync('Visu', 'UpdateValueDay15').then(
                result => {
                    value = result;
                    cube.rotation.x = value;
                    cube.rotation.y = value;
                    renderer.render(scene, camera);
                }
            )                ;
        }
        animate();
    }
};
