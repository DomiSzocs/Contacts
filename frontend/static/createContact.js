const contactName = document.getElementById('name');
const phoneNumber = document.getElementById('phone-number');
const note = document.getElementById('note');
const contactForm = document.getElementsByClassName('contact-detail')[0];

contactForm.addEventListener('submit', async () => {
  await fetch('http://localhost:5033/api/contacts/', {
    method: 'Post',
    body: JSON.stringify({
      name: contactName.value,
      phoneNumber: phoneNumber.value,
      note: note.value
    }),
    headers: {
      'Content-Type': 'application/json'
    }
  });
  window.location.href = "/";
});
