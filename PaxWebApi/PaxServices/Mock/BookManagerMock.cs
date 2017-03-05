using Entities;
using PaxDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxServices
{
    public class BookManagerMock : IBookManager
    {
        public DetailsBooksModel ComputeDetailsHeartBooks()
        {
            throw new NotImplementedException();
        }

        public BaseResultModel ComputeHeartBooksAndDetailsToFile()
        {
            throw new NotImplementedException();
        }

        public BestSellersModel GetBestSellers()
        {
            throw new NotImplementedException();
        }

        public BookDetailsItem GetDetailsBook(string completeHref)
        {
            throw new NotImplementedException();
        }

        public EventsModel GetEvents()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public HeartBooksModel GetHeartBooks()
        {
            return new HeartBooksModel
            {
                HeartBooks = new List<BookItem> {
                new BookItem {
                    Id = "1",
                    Author = "Arthur Fox",
                    Description="Le Lorem Ipsum est simplement du faux texte employé dans la composition et la mise en page avant impression. Le Lorem Ipsum est le faux texte standard de l'imprimerie depuis les années 1500, quand un peintre anonyme assembla ensemble des morceaux de texte pour réaliser un livre spécimen de polices de texte. Il n'a pas fait que survivre cinq siècles, mais s'est aussi adapté à la bureautique informatique, sans que son contenu n'en soit modifié. Il a été popularisé dans les années 1960 grâce à la vente de feuilles Letraset contenant des passages du Lorem Ipsum, et, plus récemment, par son inclusion dans des applications de mise en page de texte, comme Aldus PageMaker.",
                    Title = "Qu'est-ce que le Lorem Ipsum?"
                },
                new BookItem {
                    Id = "1",
                    Author = "Merlin Dee",
                    Description="Contrairement à une opinion répandue, le Lorem Ipsum n'est pas simplement du texte aléatoire. Il trouve ses racines dans une oeuvre de la littérature latine classique datant de 45 av. J.-C., le rendant vieux de 2000 ans. Un professeur du Hampden-Sydney College, en Virginie, s'est intéressé à un des mots latins les plus obscurs, consectetur, extrait d'un passage du Lorem Ipsum, et en étudiant tous les usages de ce mot dans la littérature classique, découvrit la source incontestable du Lorem Ipsum. Il provient en fait des sections 1.10.32 et 1.10.33 (Des Suprêmes Biens et des Suprêmes Maux) de Cicéron. Cet ouvrage, très populaire pendant la Renaissance, est un traité sur la théorie de l'éthique. Les premières lignes du Lorem Ipsum, proviennent de la section 1.10.32.",
                    Title = "D'où vient-i"
                },
                new BookItem {
                    Id = "2",
                    Author = "Roger Leming",
                    Description="Plusieurs variations de Lorem Ipsum peuvent être trouvées ici ou là, mais la majeure partie d'entre elles a été altérée par l'addition d'humour ou de mots aléatoires qui ne ressemblent pas une seconde à du texte standard. Si vous voulez utiliser un passage du Lorem Ipsum, vous devez être sûr qu'il n'y a rien d'embarrassant caché dans le texte. Tous les générateurs de Lorem Ipsum sur Internet tendent à reproduire le même extrait sans fin, ce qui fait de lipsum.com le seul vrai générateur de Lorem Ipsum. Iil utilise un dictionnaire de plus de 200 mots latins, en combinaison de plusieurs structures de phrases, pour générer un Lorem Ipsum irréprochable. Le Lorem Ipsum ainsi obtenu ne contient aucune répétition, ni ne contient des mots farfelus, ou des touches d'humour.",
                    Title = "Où puis-je m'en procurer?"
                },
                new BookItem {
                    Id = "3",
                    Author = "Bertrand Lestufel",
                    Description="L'extrait standard de Lorem Ipsum utilisé depuis le XVIè siècle est reproduit ci-dessous pour les curieux. Les sections 1.10.32 et 1.10.33 du Cicéron sont aussi reproduites dans leur version originale, accompagnée de la traduction anglaise de H. Rackham (1914).",
                    Title = "Paragraphes"
                }
            }
            };
        }
    }
}
