import morgan from 'morgan';
import express from 'express';
import { join } from 'path';
import axios from 'axios';
import https from 'https';

const app = express();

const staticDir = join(process.cwd(), 'static');

const agent = new https.Agent({ rejectUnauthorized: false });

app.use(express.static(staticDir));

app.use(morgan('tiny'));

app.get('/', (req, res) => {
  res.redirect('/contacts');
});

app.get('/contacts', async (req, res) => {
  const response = await axios.get('http://localhost:5033/api/contacts', {httpsAgent: agent});
  const contacts = response.data;
  res.status(200).render('index.ejs', { contacts });
});

app.get('/contacts/:id', async (req, res) => {
  const response = await axios.get(`http://localhost:5033/api/contacts/${req.params.id}`, {httpsAgent: agent});
  const contact = response.data;
  res.status(200).render('details.ejs', {contact});
});

app.get('/add_new_contact', (req, res) => {
  res.status(200).render('newContact.ejs', {});
})

app.listen(8080, () => { console.log('Server listening on http://localhost:8080/ ...'); });
