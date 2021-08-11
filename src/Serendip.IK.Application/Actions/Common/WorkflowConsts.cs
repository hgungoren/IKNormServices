using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Serendip.IK.Action.Common
{
    public class WorkflowConsts
    {
        public static List<TriggerGroup> Triggers = new List<TriggerGroup>
        {
            //new TriggerGroup
            //{
            //    Name = ModelTypes.LEAD,
            //    DisplayName = ModelTypes.LEAD,
            //    Triggers = new List<TriggerItem>
            //    {
            //        new TriggerItem
            //        {
            //            Name = EventNames.LEAD_CREATED,
            //            DisplayName = EventNames.LEAD_CREATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.LEAD_DELETED,
            //            DisplayName = EventNames.LEAD_DELETED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.LEAD_UPDATED,
            //            DisplayName = EventNames.LEAD_UPDATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.LEAD_STAGE_CHANGED,
            //            DisplayName = EventNames.LEAD_STAGE_CHANGED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.LEAD_STATUS_CHANGED,
            //            DisplayName = EventNames.LEAD_STATUS_CHANGED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.LEAD_TRANSFERED,
            //            DisplayName = EventNames.LEAD_TRANSFERED
            //        }
            //    }
            //},
            //new TriggerGroup
            //{
            //    Name = ModelTypes.CUSTOMER,
            //    DisplayName = ModelTypes.CUSTOMER,
            //    Triggers = new List<TriggerItem>
            //    {
            //        new TriggerItem
            //        {
            //            Name = EventNames.ACCOUNT_CREATED,
            //            DisplayName = EventNames.ACCOUNT_CREATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.ACCOUNT_UPDATED,
            //            DisplayName = EventNames.ACCOUNT_UPDATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.ACCOUNT_DELETED,
            //            DisplayName = EventNames.ACCOUNT_DELETED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.ACCOUNT_TRANSFERED,
            //            DisplayName = EventNames.ACCOUNT_TRANSFERED
            //        }
            //    }
            //},
            //new TriggerGroup
            //{
            //    Name = ModelTypes.CONTACT,
            //    DisplayName = ModelTypes.CONTACT,
            //    Triggers = new List<TriggerItem>
            //    {
            //        new TriggerItem
            //        {
            //            Name = EventNames.CONTACT_CREATED,
            //            DisplayName = EventNames.CONTACT_CREATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.CONTACT_UPDATED,
            //            DisplayName = EventNames.CONTACT_UPDATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.CONTACT_DELETED,
            //            DisplayName = EventNames.CONTACT_DELETED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.CONTACT_TRANSFERED,
            //            DisplayName = EventNames.CONTACT_TRANSFERED
            //        }
            //    }
            //},
            //new TriggerGroup
            //{
            //    Name = ModelTypes.OPPORTUNITY,
            //    DisplayName = ModelTypes.OPPORTUNITY,
            //    Triggers = new List<TriggerItem>
            //    {
            //        new TriggerItem
            //        {
            //            Name = EventNames.OPPORTUNITY_CREATED,
            //            DisplayName = EventNames.OPPORTUNITY_CREATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.OPPORTUNITY_UPDATED,
            //            DisplayName = EventNames.OPPORTUNITY_UPDATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.OPPORTUNITY_DELETED,
            //            DisplayName = EventNames.OPPORTUNITY_DELETED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.OPPORTUNITY_STAGE_CHANGED,
            //            DisplayName = EventNames.OPPORTUNITY_STAGE_CHANGED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.OPPORTUNITY_STATUS_CHANGED,
            //            DisplayName = EventNames.OPPORTUNITY_STATUS_CHANGED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.OPPORTUNITY_TRANSFERED,
            //            DisplayName = EventNames.OPPORTUNITY_TRANSFERED
            //        }
            //    }
            //},
            //new TriggerGroup
            //{
            //    Name = ModelTypes.INVOICE,
            //    DisplayName = ModelTypes.INVOICE,
            //    Triggers = new List<TriggerItem>
            //    {
            //        new TriggerItem
            //        {
            //            Name = EventNames.INVOICE_CREATED,
            //            DisplayName = EventNames.INVOICE_CREATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.INVOICE_DELETED,
            //            DisplayName = EventNames.INVOICE_DELETED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.INVOICE_UPDATED,
            //            DisplayName = EventNames.INVOICE_UPDATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.INVOICE_STAGE_CHANGED,
            //            DisplayName = EventNames.INVOICE_STAGE_CHANGED
            //        },

            //    }
            //},
            //new TriggerGroup
            //{
            //    Name = ModelTypes.PAYMENT,
            //    DisplayName = ModelTypes.PAYMENT,
            //    Triggers = new List<TriggerItem>
            //    {
            //        new TriggerItem
            //        {
            //            Name = EventNames.PAYMENT_CREATED,
            //            DisplayName = EventNames.PAYMENT_CREATED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.PAYMENT_DELETED,
            //            DisplayName = EventNames.PAYMENT_DELETED
            //        },
            //        new TriggerItem
            //        {
            //            Name = EventNames.PAYMENT_UPDATED,
            //            DisplayName = EventNames.PAYMENT_UPDATED
            //        },
            //    }
            //},
        };

        public static List<FieldItem> Fields(string modelName)
        {
            //if (modelName == ModelTypes.LEAD)
            //{
            //    //var list = GetPrimitiveFields(typeof(Leads.Lead));
            //    List<string> fields = new List<string>() { "FirstName", "LastName", "Email", "Account", "Department", "Title", "WebSite", "TaxNumber", "EmployeeCount", "AnnualRevenue", "Date", "OwnerId", "OwnerGroupId", "SourceId", "ClosedReasonId", "StatusId", "IndustryId", "Description", "ClosedDescription", "ClosedDate", "CountryId", "City", "State", "ClosedUserId" };
            //    List<FieldItem> list = GetSelectedFields(typeof(Leads.Lead), fields);

            //    return list;
            //}
            //else if (modelName == ModelTypes.CUSTOMER)
            //{
            //    //var list = GetPrimitiveFields(typeof(Customers.Customer));
            //    List<string> fields = new List<string>() { "Name", "WebSite", "Email", "PhoneNumber", "PhoneNumber1", "PhoneNumber2", "PhoneNumber3", "PhoneNumber4", "FaxNumber", "TaxNumber", "TaxOffice", "IndustryId", "EmployeeCount", "AnnualRevenue", "CountryId", "City", "State", "OwnerId", "OwnerGroupId" };
            //    List<FieldItem> list = GetSelectedFields(typeof(Customers.Account), fields);

            //    return list;
            //}
            //else if (modelName == ModelTypes.CONTACT)
            //{
            //    //var list = GetPrimitiveFields(typeof(Customers.Customer));
            //    List<string> fields = new List<string>() { "Name", "Surname", "Email", "PhoneNumber", "PhoneNumber1", "PhoneNumber2", "PhoneNumber3", "PhoneNumber4", "FaxNumber", "TaxNumber", "TaxOffice", "IdentityNumber", "BirthDate", "Title", "Department", "OwnerId", "OwnerGroupId" };
            //    List<FieldItem> list = GetSelectedFields(typeof(Customers.Account), fields);

            //    return list;
            //}
            //else if (modelName == ModelTypes.OPPORTUNITY)
            //{
            //    //var list = GetPrimitiveFields(typeof(Opportunities.Opportunity));
            //    List<string> fields = new List<string>() { "Title", "Description", "Date", "PipelineStepId", "CompanyId", "EstimatedCloseDate", "ClosedDate", "LeadDate", "LeadConvertDate", "EstimatedAmount", "FinalAmount", "StatusId", "IsArchived", "CloseReasonId", "OwnerId", "OwnerGroupId" };
            //    List<FieldItem> list = GetSelectedFields(typeof(Opportunities.Opportunity), fields);

            //    return list;
            //}
            //else if (modelName == ModelTypes.PAYMENT)
            //{
            //    //var list = GetPrimitiveFields(typeof(Payments.Payment));
            //    List<string> fields = new List<string>() { "CompanyId", "OpportunityId", "Date", "PaymentMethodId", "Amount", "Description", "UniqueNumber", "OwnerId", "OwnerGroupId" };
            //    List<FieldItem> list = GetSelectedFields(typeof(Payments.Payment), fields);

            //    return list;
            //}
            //else if (modelName == ModelTypes.INVOICE)
            //{
            //    //var list = GetPrimitiveFields(typeof(Invoices.Invoice));
            //    List<string> fields = new List<string>() { "Name", "CompanyId", "OpportunityId", "InvoiceDate", "InvoiceNo", "InvoiceNumber", "TotalAmount", "FinalAmount", "ShippingDate", "PaymentDate", "UniqueNumber", "Description", "OwnerId", "OwnerGroupId" };
            //    List<FieldItem> list = GetSelectedFields(typeof(Invoices.Invoice), fields);

            //    return list;
            //}

            return new List<FieldItem>();
        }

        static List<FieldItem> GetPrimitiveFields(Type t)
        {
            var properties = t.GetProperties().Where(x => x.PropertyType != typeof(Guid) && !x.Name.EndsWith("Id") && x.Name != "IsDeleted");
            var list = new List<FieldItem>();
            foreach (var p in properties)
            {
                list.Add(new FieldItem
                {
                    Name = p.Name,
                    DisplayName = p.Name,
                    Type = GetSimpleType(p.PropertyType)
                });
            }

            return list;
        }

        static List<FieldItem> GetSelectedFields(Type t, List<string> selectedFields)
        {
            var list = new List<FieldItem>();

            foreach (var field in selectedFields)
            {
                var simpleType = GetSimpleType(t.GetProperties().Where(x => x.Name == field).FirstOrDefault()?.PropertyType);
                FieldItem item = new FieldItem()
                {
                    Name = field,
                    DisplayName = field.EndsWith("Id") ? field.Substring(0, field.Length - 2) : field,
                    Type = simpleType,
                    Operators = GetOperatorsForType(simpleType)
                };

                if (item.Type == "select")
                {
                    //item.Values = GetValuesForSelect(GetSourcesForSelect(item.Name));
                }

                list.Add(item);
            }

            return list;
        }

        static string GetSimpleType(Type t)
        {
            if (t == typeof(string))
            {
                return "string";
            }
            //else if (t == typeof(int) || t == typeof(byte) || t == typeof(long) || t == typeof(int?) || t == typeof(byte?) || t == typeof(long?)) ***Long UserId'lerde kullanılıyor olmasından ötürü select tipinde
            else if (t == typeof(int) || t == typeof(byte) || t == typeof(int?) || t == typeof(byte?))
            {
                return "integer";
            }
            else if (t == typeof(double) || t == typeof(float) || t == typeof(decimal) || t == typeof(double?) || t == typeof(float?) || t == typeof(decimal?))
            {
                return "double";
            }
            else if (t == typeof(DateTime) || t == typeof(DateTime?))
            {
                return "datetime";
            }
            else if (t == typeof(bool) || t == typeof(bool?))
            {
                return "boolean";
            }
            else if (t == typeof(Guid) || t == typeof(Guid?) || t == typeof(long) || t == typeof(long?))
            {
                return "select";
            }
            else
            {
                return "string";
            }
        }

        static List<string> GetOperatorsForType(string type)
        {
            switch (type)
            {
                case "string":
                    return new List<string>() { "equal", "contains", "begins_with", "ends_with", "is_null", "is_not_null" };
                case "integer":
                    return new List<string>() { "equal", "greater", "greater_or_equal", "less", "less_or_equal" };
                case "double":
                    return new List<string>();
                case "datetime":
                    return new List<string>() { "equal", "greater", "less", "between", "not_between" };
                case "boolean":
                    return new List<string>() { "equal" };
                case "select":
                    return new List<string>() { "equal", "not_equal", "is_null", "is_not_null" };
                default:
                    return new List<string>();
            }
        }

        static string GetSourcesForSelect(string type)
        {
            switch (type)
            {
                case "OwnerId":
                case "ClosedUserId":
                    return "user";
                case "OwnerGroupId":
                    return "userGroup";
                case "SourceId":
                    return "tags_sources";
                case "ClosedReasonId":
                    return "tags_leadReasons";
                case "CloseReasonId":
                    return "tags_opportunityLostReasons";
                case "StatusId":
                    return "tags_leadStatuses";
                case "IndustryId":
                    return "tags_industries";
                case "CountryId":
                    return "country";
                default:
                    return "";
            }
        }

        //static Dictionary<string, string> GetValuesForSelect(string source)
        //{
        //    var data = new RecordResult();
        //    if (!source.StartsWith("tags_"))
        //    {
        //        data = RecordManager.Filter(source, new FilterContainer()
        //        {
        //            Take = int.MaxValue,
        //            OrderBy = new List<OrderFilter>
        //        {
        //            new OrderFilter
        //            {
        //                Field = "Id",
        //                Order = OrderFilterType.Desc
        //            }
        //        }
        //        });
        //    }
        //    else
        //    {
        //        var result = RecordManager.Filter("tagCategory", new FilterContainer()
        //        {
        //            Take = int.MaxValue,
        //            Where = new TreeFilter
        //            {
        //                OperatorType = TreeFilterType.And,
        //                Operands = new List<TreeFilter>
        //                {
        //                    new TreeFilter
        //                    {
        //                        Field = "Code",
        //                        FilterType = WhereFilterType.Equal,
        //                        Value = source.Split('_')[1]
        //                    }
        //                }
        //            }
        //        });

        //        data = RecordManager.Filter("tag", new FilterContainer()
        //        {
        //            Take = int.MaxValue,
        //            Where = new TreeFilter
        //            {
        //                OperatorType = TreeFilterType.And,
        //                Operands = new List<TreeFilter>
        //                {
        //                    new TreeFilter
        //                    {
        //                        Field = "CategoryId",
        //                        FilterType = WhereFilterType.Equal,
        //                        Value = result.Items.FirstOrDefault().GetType().GetProperty("Id").GetValue(result.Items.FirstOrDefault())
        //                    }
        //                }
        //            }
        //        });
        //    }

        //    Dictionary<string, string> values = new Dictionary<string, string>();

        //    foreach (var item in data.Items)
        //    {
        //        values.Add(item.GetType().GetProperty("Id").GetValue(item).ToString(), item.GetType().GetProperty("Name").GetValue(item)?.ToString());
        //    }

        //    return values;
        //}
    }
}
