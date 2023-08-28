const contacts = Array.from(document.getElementsByClassName('contact-entry'));
const searchInput = document.getElementById('search-input');
const newContactButton = document.getElementById('add-contact');

function escapeRegExp(string) {
  return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // Escapes special characters
}

searchInput.addEventListener('input', (event) => {
  const searchString = escapeRegExp(event.target.value);
  const regex = new RegExp(searchString, "i");
  contacts.forEach(element => {
    const [nameElement, phoneNumberElement] = element.children;
    const name = nameElement.children[0].innerText;
    const phoneNumber = phoneNumberElement.children[0].innerText;
    element.style.display = regex.test(name) || regex.test(phoneNumber) ? 'block' : 'none';
  });
});
