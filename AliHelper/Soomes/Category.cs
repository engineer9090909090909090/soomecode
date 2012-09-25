using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soomes
{

    public class Category
    {
        public string id;
        public string title;
        public bool hasPrivilege;
        public string warnMessage;
        public Category parentNode;
        public List<Category> childCategorys;
        public string longTitle;

        public Category(Category parentNode, string id,string title,string hasPrivilege,string warnMessage)
        { 
            this.id=id;
            this.title = title;
            if (hasPrivilege == "true") {
                this.hasPrivilege = true;
            } else {
                this.hasPrivilege = false;
            }
	        this.warnMessage = warnMessage;
            this.childCategorys = new List<Category>();
            if (parentNode != null)
            {
                this.applyParent(parentNode);
            }
            if (parentNode != null)
            {
                if (string.IsNullOrEmpty(parentNode.longTitle))
                {
                    this.longTitle =  title;
                }
                else
                {
                    this.longTitle = parentNode.longTitle + " -> " + title;
                }
            }
        }

        public void applyParent(Category parentNode )
        {		
	        this.parentNode = parentNode;
		    this.parentNode.addChild( this );
	    }

	    public void addChild(Category node )
        {
		    this.childCategorys.Add( node );
	    }
    }
}
