<template>
  <div class="hello">
    <h1>Contatos {{ msg }}</h1>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Nome</th>
          <th>Telefone</th>
          <th>EMail</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="contato in Contatos" v-bind:key="contato.id">
          <td>{{contato.id}}</td>
          <td>{{contato.nome}}</td>
          <td>{{contato.telefone}}</td>
          <td>{{contato.email}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'Contatos',
  props: {
    msg: String
  },
  data: () =>{
    return {
      Contatos: [],
    }
  },
  methods: {
    listarContatos: () => {
      const urlListarAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Listar'
      // const urlListarLocal = 'https://localhost:44364/api/Agenda/Listar'
      
      axios.get(urlListarAzure).then((listaContatos) => {
        console.log(listaContatos.data);
      })
    }
  },
  created(){
    this.listarContatos(this)
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
