using Serendip.IK.Units;
using System.Collections.Generic;
using System.Linq;

namespace Serendip.IK.EntityFrameworkCore.Seed.Host
{
    public class KHierarchyCreator
    {
        private readonly IKDbContext _context;

        public KHierarchyCreator(IKDbContext context) { _context = context; }
        public void Create() { CreateEditions(); }

        private void CreateEditions()
        {

            #region Görevler
            /// <summary>
            /// Bölge Müdür Yrd. - İnsan Kaynakları
            /// </summary> 
            const string BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI = "Bölge Müdür Yrd. - İnsan Kaynakları";

            /// <summary>
            /// Bölge Müdürü
            /// </summary>
            const string BOLGE_MUDURU = "Bölge Müdürü";

            /// <summary>
            /// Genel Müdürlük Operasyon Müdürü
            /// </summary>
            const string DEPARTMAN_MUDURU = "Operasyon Müdürü";

            /// <summary>
            /// Genel Müdürlük Operasyon Müdürü
            /// </summary>
            const string GENEL_MUDUR_YRD = "Genel Müdür Yrd.";

            /// <summary>
            /// İnsan Kaynakları ve İşe Alım Müdür Yrd
            /// </summary>
            const string INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD = "İnsan Kaynakları ve İşe Alım Müdür Yrd";

            /// <summary>
            /// İnsan Kaynakları ve İşe Alım Müdürü
            /// </summary>
            const string INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU = "İnsan Kaynakları ve İşe Alım Müdürü";

            /// <summary>
            /// Mikro Operasyonları Müdürü
            /// </summary>
            const string MIKRO_OPERASYONLARI_MUDURU = "Mikro Operasyonları Müdürü";

            /// <summary>
            /// Aktarma Operasyonları Müdürü
            /// </summary>
            const string AKTARMA_OPERASYONLARI_MUDURU = "Aktarma Operasyonları Müdürü";


            /// <summary>
            /// Genel Müdür
            /// </summary>
            const string GENEL_MUDUR = "Genel Müdür";
            #endregion


            if (_context.KHierarchies.Count() > 0)
            {
                return;
            }

            // Şube
            var hierarchiesBranch = new List<KHierarchy>
                {
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI",        Title = BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI ,           KHierarchyType = KHierarchyType.Branch, OrderNo = 1,               EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDURU",                            Title = BOLGE_MUDURU ,                               KHierarchyType = KHierarchyType.Branch, OrderNo = 2, MasterId = 1, EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "DEPARTMAN_MUDURU",                        Title = DEPARTMAN_MUDURU ,                           KHierarchyType = KHierarchyType.Branch, OrderNo = 3, MasterId = 2, EndApprove = false ,            Mail = "fatih.yaz@suratkargo.com.tr",       FirstName = "Fatih", LastName = "Yaz"},
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Branch, OrderNo = 4, MasterId = 3, EndApprove = false ,            Mail = "tolga.karaduman@suratkargo.com.tr", FirstName = "Tolga", LastName = "Karaduman", GMYType = GMYType.Operasyon},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD",  Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD ,     KHierarchyType = KHierarchyType.Branch, OrderNo = 5, MasterId = 4, EndApprove = false ,            Mail = "ersin.eryilmaz1@suratkargo.com.tr", FirstName = "Ersin", LastName= "Eryılmaz"} ,
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU",     Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU ,        KHierarchyType = KHierarchyType.Branch, OrderNo = 6, MasterId = 5, EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Branch, OrderNo = 7, MasterId = 6, EndApprove = true ,             Mail = "engin.aktepe@suratkargo.com.tr",    FirstName = "Engin", LastName = "Aktepe",    GMYType = GMYType.IK}
                };

            // Acente                                                                                                                                                                                                                                     
            var hierarchiesAgent = new List<KHierarchy>
                {
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI", Title = BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI ,    KHierarchyType = KHierarchyType.Agent, OrderNo = 1,                EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDURU",                            Title = BOLGE_MUDURU ,                               KHierarchyType = KHierarchyType.Agent, OrderNo = 2, MasterId = 9,  EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "DEPARTMAN_MUDURU",                        Title = DEPARTMAN_MUDURU ,                           KHierarchyType = KHierarchyType.Agent, OrderNo = 3, MasterId = 10, EndApprove = false ,            Mail = "fatih.yaz@suratkargo.com.tr", FirstName = "Fatih", LastName = "Yaz"},
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Agent, OrderNo = 4, MasterId = 11, EndApprove = false ,            Mail = "tolga.karaduman@suratkargo.com.tr" , FirstName = "Tolga",LastName = "Karaduman", GMYType = GMYType.Operasyon},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD",  Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD ,     KHierarchyType = KHierarchyType.Agent, OrderNo = 5, MasterId = 12, EndApprove = true ,             Mail = "ersin.eryilmaz1@suratkargo.com.tr", FirstName = "Ersin", LastName= "Eryılmaz"} ,
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU",     Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU ,        KHierarchyType = KHierarchyType.Agent, OrderNo = 6, MasterId = 13, EndApprove = true } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Agent, OrderNo = 7, MasterId = 14, EndApprove = true ,             Mail = "engin.aktepe@suratkargo.com.tr" ,FirstName = "Engin", LastName = "Aktepe", GMYType = GMYType.IK}
                };

            // Mikro
            var hierarchiesMicro = new List<KHierarchy>
                {
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI", Title = BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI ,    KHierarchyType = KHierarchyType.Micro, OrderNo = 1 ,                EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDURU",                            Title = BOLGE_MUDURU ,                               KHierarchyType = KHierarchyType.Micro, OrderNo = 2, MasterId = 16 , EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "MIKRO_OPERASYONLARI_MUDURU",              Title = MIKRO_OPERASYONLARI_MUDURU ,                 KHierarchyType = KHierarchyType.Micro, OrderNo = 3, MasterId = 17 , EndApprove = false ,           Mail = "fatih.yaz@suratkargo.com.tr", FirstName = "Fatih", LastName = "Yaz"},
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Micro, OrderNo = 4, MasterId = 18 , EndApprove = false ,           Mail = "tolga.karaduman@suratkargo.com.tr", FirstName = "Tolga",LastName = "Karaduman", GMYType = GMYType.Operasyon},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD",  Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD ,     KHierarchyType = KHierarchyType.Micro, OrderNo = 5, MasterId = 19 , EndApprove = true ,            Mail = "ersin.eryilmaz1@suratkargo.com.tr", FirstName = "Ersin", LastName= "Eryılmaz"},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU",     Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU ,        KHierarchyType = KHierarchyType.Micro, OrderNo = 6, MasterId = 20 , EndApprove = true } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Micro, OrderNo = 7, MasterId = 21 , EndApprove = true ,            Mail = "engin.aktepe@suratkargo.com.tr",FirstName = "Engin", LastName = "Aktepe", GMYType = GMYType.IK}
                };

            // Aktarma                                                                                                                                                                                                                                    
            var hierarchiesTransfer = new List<KHierarchy>
                {
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI", Title = BOLGE_MUDUR_YRD_INSAN_KAYNAKLARI ,    KHierarchyType = KHierarchyType.Transfer, OrderNo = 1 ,                EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDURU",                            Title = BOLGE_MUDURU ,                               KHierarchyType = KHierarchyType.Transfer, OrderNo = 2, MasterId = 23 , EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "AKTARMA_OPERASYONLARI_MUDURU",            Title = AKTARMA_OPERASYONLARI_MUDURU ,               KHierarchyType = KHierarchyType.Transfer, OrderNo = 3, MasterId = 24 , EndApprove = false ,        Mail = "fatih.yaz@suratkargo.com.tr", FirstName = "Fatih", LastName = "Yaz"},
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Transfer, OrderNo = 4, MasterId = 25 , EndApprove = false ,        Mail = "tolga.karaduman@suratkargo.com.tr", FirstName = "Tolga",LastName = "Karaduman", GMYType = GMYType.Operasyon},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD",  Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD ,     KHierarchyType = KHierarchyType.Transfer, OrderNo = 5, MasterId = 26 , EndApprove = true ,         Mail = "ersin.eryilmaz1@suratkargo.com.tr", FirstName = "Ersin", LastName= "Eryılmaz"},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU",     Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU ,        KHierarchyType = KHierarchyType.Transfer, OrderNo = 7, MasterId = 28 , EndApprove = true , },
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Transfer, OrderNo = 7, MasterId = 21 , EndApprove = true ,         Mail = "engin.aktepe@suratkargo.com.tr",FirstName = "Engin", LastName = "Aktepe", GMYType = GMYType.IK}
                };

            // Bölge                                                                                                                                                                                                                                      
            var hierarchiesArea = new List<KHierarchy>
                {
                    new KHierarchy { NormalizedTitle = "BOLGE_MUDURU",                            Title = BOLGE_MUDURU ,                               KHierarchyType = KHierarchyType.Area, OrderNo = 1,                 EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "DEPARTMAN_MUDURU",                        Title = DEPARTMAN_MUDURU ,                           KHierarchyType = KHierarchyType.Area, OrderNo = 2, MasterId = 30 , EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Area, OrderNo = 3, MasterId = 31 , EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD",  Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD ,     KHierarchyType = KHierarchyType.Area, OrderNo = 4, MasterId = 32 , EndApprove = true ,             Mail = "ersin.eryilmaz1@suratkargo.com.tr", FirstName = "Ersin", LastName= "Eryılmaz"},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU",     Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU ,        KHierarchyType = KHierarchyType.Area, OrderNo = 5, MasterId = 33 , EndApprove = true  } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.Area, OrderNo = 6, MasterId = 34 , EndApprove = true ,             Mail = "engin.aktepe@suratkargo.com.tr",FirstName = "Engin", LastName = "Aktepe", GMYType = GMYType.IK},
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR",                             Title = GENEL_MUDUR ,                                KHierarchyType = KHierarchyType.Area, OrderNo = 7, MasterId = 35 , EndApprove = true }
                };

            // Genel Müdürlük                                                                                                                                                                                                                             
            var hierarchiesGeneralManager = new List<KHierarchy>
                {
                    new KHierarchy { NormalizedTitle = "DEPARTMAN_MUDURU",                        Title = DEPARTMAN_MUDURU ,                           KHierarchyType = KHierarchyType.GeneralManager, OrderNo = 1,                 EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.GeneralManager, OrderNo = 2, MasterId = 37 , EndApprove = false } ,
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD",  Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDUR_YRD,      KHierarchyType = KHierarchyType.GeneralManager, OrderNo = 3, MasterId = 38 , EndApprove = true ,    Mail = "ersin.eryilmaz1@suratkargo.com.tr", FirstName = "Ersin", LastName= "Eryılmaz"},
                    new KHierarchy { NormalizedTitle = "INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU",     Title = INSAN_KAYNAKLARI_VE_ISE_ALIM_MUDURU ,        KHierarchyType = KHierarchyType.GeneralManager, OrderNo = 4, MasterId = 39 , EndApprove = true } ,
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR_YRD",                         Title = GENEL_MUDUR_YRD ,                            KHierarchyType = KHierarchyType.GeneralManager, OrderNo = 5, MasterId = 40 , EndApprove = true ,    Mail = "engin.aktepe@suratkargo.com.tr",FirstName = "Engin", LastName = "Aktepe", GMYType = GMYType.IK},
                    new KHierarchy { NormalizedTitle = "GENEL_MUDUR",                             Title = GENEL_MUDUR ,                                KHierarchyType = KHierarchyType.GeneralManager, OrderNo = 6, MasterId = 41 , EndApprove = true }
                };



            _context.KHierarchies.AddRange(hierarchiesBranch);
            _context.KHierarchies.AddRange(hierarchiesAgent);
            _context.KHierarchies.AddRange(hierarchiesMicro);
            _context.KHierarchies.AddRange(hierarchiesTransfer);
            _context.KHierarchies.AddRange(hierarchiesArea);
            _context.KHierarchies.AddRange(hierarchiesGeneralManager);
            _context.SaveChanges();


        }
    }
}
