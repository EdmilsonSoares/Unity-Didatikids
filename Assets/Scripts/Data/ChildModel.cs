// ChildProfileModel.cs
using System;

[System.Serializable]
public class ChildModel
{
    public string childNome;
    public string childData; // Data de nascimento da criança
    public string childTopico;
    public string avatarIconPath;  // Caminho para o arquivo PNG do avatar

    public ChildModel(string nome, string data, string topico, string iconPath)
    {
        this.childNome = nome;
        this.childData = data;
        this.childTopico = topico;
        this.avatarIconPath = iconPath;
    }
}