// Classe modelo para os dados do usuário
using System;
using System.Collections.Generic; // Necessário para List

[System.Serializable]
public class UserModel
{
    public string userNome;
    public string userDataNascimento;
    public string userEmail;
    public string userSenha; // Em um aplicativo real, nunca salve a senha em texto puro! Use hashing.
    
    public List<ChildModel> children;// Lista para armazenar os perfis das crianças associadas a este responsável
    public UserModel(string nome, string dataNascimento, string email, string senha)
    {
        this.userNome = nome;
        this.userDataNascimento = dataNascimento;
        this.userEmail = email;
        this.userSenha = senha;
        this.children = new List<ChildModel>(); // Inicializa a lista vazia
    }
    // Método para adicionar um novo perfil de criança
    public void AddChildProfile(ChildModel child)
    {
        if (children == null)
        {
            children = new List<ChildModel>(); // Garante que a lista não é nula
        }
        children.Add(child);
    }
}