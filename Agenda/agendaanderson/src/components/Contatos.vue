<template>
  <div class="hello">
    <h1>Contatos {{ msg }}</h1>
    <div class='container'>
    <form action="">
      <div class='form-group'>
        <input id='id' name='id' type='hidden'>
        <label for="nome">Nome</label>
        <input type="text" class='form-control' id='nome' name='nome' placeholder="Digite o Nome">
      </div>
      <div class='form-group'>
        <label for="telefone">Telefone</label>
        <input type="tel" class='form-control' id='telefone' name='telefone' placeholder="Digite o Telefone" maxlength="14">
      </div>
        <div class='form-group'>
        <label for="telefone">E-Mail</label>
        <input type="email" class='form-control' id='email' name='email' placeholder="Digite o E-Mail">
      </div>
      <br>
      <button type='button' v-on:click="salvarContato()" class='btn btn-primary'>Salvar</button>
    </form>

    </div>
    <hr>
    <table class='table'>
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
            <button class='btn btn-success' @click="consultarContato(contato.id);">Editar</button>
            &nbsp;
            |
            &nbsp;
            <button class='btn btn-danger' @click="removerContato(contato.id);">Remover</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
var escopoPagina = null;
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
    salvarContato: () => {
      var id = document.getElementById('id').value;

      if(id != null && id != undefined){
        if(id > 0){
          escopoPagina.atualizarContato(id);
        }else{
          escopoPagina.inserirContato();
        }
      }
    },
    inserirContato: () => {
      const urlInserirAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Inserir';

      var pContato = {
        Id: -1,
        Nome: document.getElementById('nome').value,
        Telefone: document.getElementById('telefone').value,
        Email: document.getElementById('email').value,
      };

      console.log(pContato);

      axios.post(urlInserirAzure, pContato).then((contatoInserido) => {
        console.log('Inserido ' + contatoInserido.data.id);  
        console.log(contatoInserido.data);
        escopoPagina.listarContatos();   
        escopoPagina.limparFormulario();   
        alert('Contato inserido.'); 
      });
    },
    consultarContato: (pIdContato) => {
      escopoPagina.limparFormulario();
      const urlConsultarAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Consultar?pIdContato=' + pIdContato

        axios.get(urlConsultarAzure).then((contatoConsultado) => {
          console.log('Consultado ' + pIdContato);  
          console.log(contatoConsultado.data);
          
          document.getElementById('id').value = contatoConsultado.data.id;
          document.getElementById('nome').value = contatoConsultado.data.nome;
          document.getElementById('telefone').value = contatoConsultado.data.telefone;
          document.getElementById('email').value = contatoConsultado.data.email;
        });
    },
    listarContatos: () => {
      const urlListarAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Listar'
      
      axios.get(urlListarAzure).then((listaContatos) => {
        console.log('Listado');
        console.log(listaContatos);
        escopoPagina.Contatos = listaContatos.data;
      });
    },
    atualizarContato: () => {
      const atualizarContatoAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Atualizar';

      var pContato = {
        Id: document.getElementById('id').value,
        Nome: document.getElementById('nome').value,
        Telefone: document.getElementById('telefone').value,
        Email: document.getElementById('email').value,
      };

      axios.put(atualizarContatoAzure, pContato).then((contatoAtualizado) => {
        console.log('Atualizado ' + contatoAtualizado.id);  
        console.log(contatoAtualizado.data);
        escopoPagina.listarContatos();   
        escopoPagina.limparFormulario();
        alert('Contato atualizado.'); 
      });
    },
    removerContato: (pIdContato) => {
        const urlRemoverAzure = 'https://apiagenda.azurewebsites.net/api/Agenda/Remover?pIdContato=' + pIdContato

        axios.delete(urlRemoverAzure).then(() => {
          console.log('Removido ' + pIdContato);   
          escopoPagina.listarContatos(); 
          alert('Contato removido.');
        });
    },
    limparFormulario: () => {
        document.getElementById('id').value = '';
        document.getElementById('nome').value = '';
        document.getElementById('telefone').value = '';
        document.getElementById('email').value = '';
    }
  },
  created(){
    escopoPagina = this;
    this.listarContatos()
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
