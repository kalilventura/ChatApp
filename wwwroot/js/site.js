let createRoomButton = document.getElementById('create-room-btn')
let createRoomModal = document.getElementById('create-room-modal')

createRoomButton.addEventListener('click', function () {
    createRoomModal.classList.add('active')
})

let closeModal = function () {
    createRoomModal.classList.remove('active')
}