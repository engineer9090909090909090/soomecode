using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{
    public class ProductDetail
    {
        public int pid { set; get; }
        public string gmtModified { set; get; }
        public FormElement fromType2 { set; get; }//fromType2
        public FormElement commonCategoryName { set; get; }//_fmp.pr._0.ca
        public FormElement commonCategoryIds { set; get; }//_fmp.pr._0.c
        public FormElement canChangeWaterMark { set; get; } //canChangeWaterMark
        public FormElement versionId { set; get; } //versionId
        public FormElement categoryIdsPathStr { set; get; }//categoryIdsPathStr
        public FormElement categoryDisappeared { set; get; }//categoryDisappeared
        public FormElement trashDOId { set; get; }//trashDOId
        public FormElement isFromTrash { set; get; }//isFromTrash
        public FormElement trashXmlPath { set; get; }//trashXmlPath
        public FormElement event_submit_do_change_category { set; get; }//event_submit_do_change_category
        public FormElement productQueryURL { set; get; }//productQueryURL
        public FormElement productSketchId { set; get; }//productSketchId
        public FormElement hiddenCatPath { set; get; }//hiddenCatPath

        public FormElement groupId { set; get; }//_fmp.pr._0.i
        public FormElement groupStatusId { set; get; }//_fmp.pr._0.st
        public FormElement keyFromSmartKeywords { set; get; }//keyFromSmartKeywords
        public FormElement pageType { set; get; }//pageType

        public FormElement id { set; get; }//id
        public FormElement productId { set; get; }//productId
        public FormElement productName { set; get; }//_fmp.pr._0.s
        public FormElement productKeyword { set; get; }//_fmp.pr._0.k
        public FormElement keywords2 { set; get; }//_fmp.pr._0.p
        public FormElement keywords3 { set; get; }//_fmp.pr._0.pr
        public FormElement imageFiles { set; get; }//_fmp.pr._0.ima

        public Dictionary<FormElement, FormElement> CustomAttr { set; get; }//_fmp.pr._0.u,pr._0.us

        public FormElement paymentMethod1 { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethod2 { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethod3 { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethod4 { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethod5 { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethod6 { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethodOther { set; get; }//_fmp.pr._0.pa
        public FormElement paymentMethodOtherDesc { set; get; }//_fmp.pr._0.paym

        public FormElement minOrderQuantity { set; get; }//_fmp.pr._0.mi
        public FormElement minOrderUnit { set; get; }//_fmp.pr._0.min
        public FormElement moneyType { set; get; }//_fmp.pr._0.mo
        public FormElement minOrderOther { set; get; }//_fmp.pr._0.minor
        public FormElement priceRangeMin { set; get; }//_fmp.pr._0.pri
        public FormElement priceRangeMax { set; get; }//_fmp.pr._0.pric
        public FormElement priceUnit { set; get; }//_fmp.pr._0.price
        public FormElement port { set; get; }//_fmp.pr._0.po


        /// <summary>
        /// 图片与水印
        /// </summary>
        public FormElement static_and_dyn0 { set; get; }//_fmp.pr._0.im
        public FormElement static_and_dyn1 { set; get; }//_fmp.pr._0.im
        public FormElement staticImageWaterMarkId { set; get; }//_fmp.pr._0.stat
        public FormElement staticImageToBankId { set; get; }//_fmp.pr._0.statici
        public FormElement fmppr0stati_y { set; get; }//_fmp.pr._0.stati
        public FormElement fmppr0stati_n { set; get; }//_fmp.pr._0.stati
        public FormElement fmppr0static_center { set; get; }//_fmp.pr._0.static
        public FormElement fmppr0static_down { set; get; }//_fmp.pr._0.static
        public FormElement dynamicImageWaterMarkId { set; get; }//_fmp.pr._0.dy
        public FormElement dynamicImageToBankId { set; get; }//_fmp.pr._0.dynam
        public FormElement fmppr0dyn_y { set; get; }//_fmp.pr._0.dyn
        public FormElement fmppr0dyn_n { set; get; }//_fmp.pr._0.dyn
        public FormElement fmppr0dyna_center { set; get; }//_fmp.pr._0.dyna
        public FormElement fmppr0dyna_down { set; get; }//_fmp.pr._0.dyna


        public FormElement supplyQuantity { set; get; }//_fmp.pr._0.sup
        public FormElement supplyPeriod { set; get; }//_fmp.pr._0.supply
        public FormElement supplyUnit { set; get; }//_fmp.pr._0.supp
        public FormElement supplyOther { set; get; }//_fmp.pr._0.supplyo
        public FormElement consignmentTerm { set; get; }//_fmp.pr._0.co
        public FormElement productTeamInputBox { set; get; }//_fmp.pr._0.grou
        public FormElement productGroupId1 { set; get; }//_fmp.pr._0.g
        public FormElement productGroupId2 { set; get; }//_fmp.pr._0.gr
        public FormElement productGroupId3 { set; get; }//_fmp.pr._0.gro


        public FormElement summary { set; get; }//_fmp.pr._0.su
        public FormElement productDescriptionTemp { set; get; }//_fmp.pr._0.de
        public FormElement packagingDesc { set; get; }//_fmp.pr._0.pac


        public FormElement fmppr0r { set; get; }//_fmp.pr._0.r
        public FormElement fmppr0is { set; get; }//_fmp.pr._0.is

        public FormElement dynamicImageTbdFlag { set; get; }//dynamicImageTbdFlag
        public FormElement dynamicImageOriginFlag { set; get; }//dynamicImageOriginFlag
        public FormElement dynamicImageChangedFlag { set; get; }//dynamicImageChangedFlag
        public FormElement fromvirtualsite { set; get; }//fromvirtualsite
        public FormElement userReviseCategory { set; get; }//userReviseCategory
        public FormElement backUrl { set; get; }//backUrl

        public List<AttributeNode> SysAttr { set; get; }
        public List<AttributeNode> FixAttr { set; get; }
    }
}