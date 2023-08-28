const contactName = document.getElementById('name');
const phoneNumber = document.getElementById('phone-number');
const note = document.getElementById('note');
const editButton = document.getElementById('edit-button');
const saveButton = document.getElementById('save-button');
const deleteButton = document.getElementById('delete-button');
const id = document.getElementsByClassName('contact-detail')[0].id;

editButton.addEventListener('click', (event) => {
  event.target.style.display = 'none';
  contactName.disabled = false;
  phoneNumber.disabled = false;
  note.disabled = false;
  saveButton.style.display = 'inline-block';
});

saveButton.addEventListener('click', async (event) => {
  await fetch(`http://localhost:5033/api/contacts/${id}`, {
    method: 'Put',
    body: JSON.stringify({
      name: contactName.value,
      phoneNumber: phoneNumber.value,
      note: note.value
    }),
    headers: {
      'Content-Type': 'application/json'
    }
  });

  event.target.style.display = 'none';
  contactName.disabled = true;
  phoneNumber.disabled = true;
  note.disabled = true;
  editButton.style.display = 'inline-block';
});

deleteButton.addEventListener('click', async () => {
  await fetch(`http://localhost:5033/api/contacts/${id}`, {method: 'Delete'});
  window.location.href = "/";
})
