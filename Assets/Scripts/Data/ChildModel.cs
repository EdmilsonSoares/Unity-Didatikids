// ChildProfileModel.cs
using System;

[System.Serializable]
public class ChildModel
{
    public string childNome;
    public string childData; // Data de nascimento da criança
    public string avatarIconPath;   // Caminho para o arquivo PNG do avatar

    public ChildModel(string nome, string data, string iconPath)
    {
        this.childNome = nome;
        this.childData = data;
        this.avatarIconPath = iconPath;
    }
}