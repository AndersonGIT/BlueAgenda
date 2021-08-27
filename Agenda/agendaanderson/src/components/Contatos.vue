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
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="contato in Contatos" v-bind:key="contato.id">
          <td>{{contato.id}}</td>
          <td>{{contato.nome}}</td>
          <td>{{contato.telefone}}</td>
          <td>{{contato.email}}</td>
          <td>
            &nbsp;
            <button @click="atualizarContato()">Editar</button>
            &nbsp;
            <button @click="removerContato(contato.id);">Remover</button>
          </td>
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
    inserirContato: (pContato) => {
      const urlInserirAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Inserir';

        console.log(pContato);

        axios.get(urlInserirAzure).then((contatoInserido) => {
          console.log('Inserido ' + contatoInserido.id);  
          console.log(contatoInserido.data)      
        });

    },
    consultarContato: (pIdContato) => {
      const urlConsultarAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Consultar?pIdContato' + pIdContato

        axios.get(urlConsultarAzure).then((contatoConsultado) => {
          console.log('Consultado ' + pIdContato);  
          console.log(contatoConsultado.data)      
        });

    },
    listarContatos: (pEscopo) => {
      const urlListarAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Listar'
      
      axios.get(urlListarAzure).then((listaContatos) => {
        console.log(listaContatos);
        pEscopo.Contatos = listaContatos.data;
      });
    },
    atualizarContato: () => {

    },
    removerContato: (pIdContato) => {
        const urlRemoverAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Remover?pIdContato=' + pIdContato

        axios.delete(urlRemoverAzure).then(() => {
          console.log('Removido ' + pIdContato);        
        });
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
