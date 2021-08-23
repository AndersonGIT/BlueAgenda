using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LayerLib.Classes.Extensao
{
    public static class Extension
    {
        #region Extensões para o tipo Enum

        /// <summary>
        /// Método para transformar um enum no conteúdo de seu atributo "description"
        /// </summary>
        /// <param name="item">Enum</param>
        /// <returns>Representação em formato de texto baseado no conteúdo de seu atributo "description"</returns>
        public static string ToDescription(this Enum item)
        {
            FieldInfo fi = item.GetType().GetField(item.ToString());
            DescriptionAttribute[] atributosValor = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            string valorEnum = item.ToString();

            if (atributosValor.Length > 0)
                valorEnum = Convert.ToString(atributosValor[0].Description);

            return valorEnum;
        }

        /// <summary>
        /// Método para transformar um enum no conteúdo de seu atributo "default value"
        /// </summary>
        /// <param name="enumerator">Enum</param>
        /// <returns>Representação em formato de texto baseado no conteúdo de seu atributo "default value"</returns>
        public static string ToDefaultValue(this Enum enumerator)
        {
            FieldInfo fi = enumerator.GetType().GetField(enumerator.ToString());
            DefaultValueAttribute[] atributosValor = fi.GetCustomAttributes(typeof(DefaultValueAttribute), false) as DefaultValueAttribute[];

            string valorEnum = enumerator.ToString();

            if (atributosValor.Length > 0)
                valorEnum = Convert.ToString(atributosValor[0].Value);

            return valorEnum;
        }

        #endregion

        #region Extensões para o tipo String

        /// <summary>
        /// Método para retornar um item de um enum com base no conteúdo do atributo description informado
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <param name="description">Descrição do item para localização</param>
        /// <returns>Item do enum localizado com base no conteúdo informado</returns>
        public static T GetEnumValueFromDescription<T>(this string description)
        {
            System.Reflection.MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            throw new Exception("Item Não encontrado.");
        }

        /// <summary>
        /// Método para retornar um item de um enum com base no conteúdo do atributo default value informado
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <param name="value">Default value do item para localização</param>
        /// <returns>Item do enum localizado com base no conteúdo informado</returns>
        public static T GetEnumValueFromDefaultValue<T>(this string value)
        {
            System.Reflection.MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                DefaultValueAttribute[] attributes = (DefaultValueAttribute[])fi.GetCustomAttributes(typeof(DefaultValueAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Value.ToString() == value)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            throw new Exception("Extensão Default não encontrada");
        }

        /// <summary>
        /// Método para retornar a string com o tamanho especificado caso ela possua um tamanho maior
        /// </summary>
        /// <param name="valor">Valor a ser truncada</param>
        /// <param name="tamanhoMaximo">Tamanho máximo para o retorno</param>
        /// <returns>String truncada</returns>
        public static string Truncate(this string valor, int tamanhoMaximo)
        {
            if (valor.Length > tamanhoMaximo)
                return valor.Substring(0, tamanhoMaximo);
            else
                return valor;
        }

        /// <summary>
        /// Método para sverificar se o item está informado no array "params"
        /// </summary>
        /// <typeparam name="T"> Tipo do parametro (Ex.: int, string)</typeparam>
        /// <param name="item">Objeto proprio</param>
        /// <param name="items">Array de objetos a comparar</param>
        /// <returns>Se contem true, se não False</returns>
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            return items.Contains(item);
        }


        #endregion

    }
}
