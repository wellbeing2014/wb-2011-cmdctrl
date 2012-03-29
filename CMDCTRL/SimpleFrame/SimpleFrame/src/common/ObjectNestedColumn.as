package common
{
	import mx.controls.dataGridClasses.DataGridColumn;

	public class ObjectNestedColumn extends DataGridColumn
	{
		public function ObjectNestedColumn(columnName:String=null)
		{
			super(columnName);
		}
		 override public function itemToLabel(data:Object):String{
   if (dataField.indexOf(".") != -1){
    var fields:Array = dataField.split(".");
    var currentData:Object=data;
    if(currentData!=null){
              currentData=splitData(currentData,dataField);
              currentData=currentData[fields[fields.length-1]];
       }
          var label:String;
          if (currentData is String){
              label = String(currentData);
          }
          try{
              label = currentData.toString();
          }catch(e:Error){
           return "";
          }
          return label;
     }
      return super.itemToLabel(data);
    }
    public function splitData(currentData:Object,srcField:String):Object{
        var currentData:Object=currentData;
        if(srcField.indexOf(".")!=-1){
          var field:String="";
          var subfield:String="";
          field=srcField.substr(0,srcField.indexOf(".")+1).replace(".","");
           currentData=currentData[field];
          subfield=srcField.substr(srcField.indexOf(".")+1,srcField.length);
          return splitData(currentData,subfield);
        }else{
          return currentData;
        }
    }

		
	}
}