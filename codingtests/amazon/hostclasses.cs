using System;
using System.Collections.Generic;

// INPUT
//AWS-EC2-IAD6 -> AWS-EC2-IAD
//AWS-EC2-IAD7 -> AWS-EC2-IAD
//AWS-EC2-IAD -> AWS-EC2
//AWS-EC2-PDX1 -> AWS-EC2-PDX
//AWS-EC2-PDX -> AWS-EC2

// OUTPUT
//AWS-EC2=5, AWS-EC2-IAD=2, AWS-EC2-IAD7=0, AWS-EC2-PDX1=0, AWS-EC2-IAD6=0, AWS-EC2-PDX=1

class MainClass {
  public static void Main (string[] args) {

    var list = new List<HostClassPair>();
    list.Add(new HostClassPair { Child = "AWS-EC2-IAD6", Parent = "AWS-EC2-IAD"});
    list.Add(new HostClassPair { Child = "AWS-EC2-IAD7", Parent = "AWS-EC2-IAD"});
    list.Add(new HostClassPair { Child = "AWS-EC2-IAD", Parent = "AWS-EC2"});
    list.Add(new HostClassPair { Child = "AWS-EC2-PDX1", Parent = "AWS-EC2-PDX"});
    list.Add(new HostClassPair { Child = "AWS-EC2-PDX", Parent = "AWS-EC2"});

    var hostClasses = new HostClasses();
    var counts = hostClasses.HostClassCounter(list);

    foreach(var item in counts) 
    {
      Console.WriteLine($"{item.Name} - {item.Count}");
    }
  }
}
  
public class HostClasses
{
    private List<Node> _nodes;
    private Dictionary<string,Node> _nodeMap;
    
    public List<HostClassPairCount> HostClassCounter(List<HostClassPair> hostClasses)
    {
        
        _nodes = new List<Node>();
        _nodeMap = new Dictionary<string,Node>();
        
        foreach(var pair in hostClasses)
        {
            Node parentNode = null;
            if(_nodeMap.ContainsKey(pair.Parent))
            {
              parentNode = _nodeMap[pair.Parent];
            }
            Node childNode = null;
            if(_nodeMap.ContainsKey(pair.Child))
            {
              childNode = _nodeMap[pair.Child];
            }

            if(parentNode != null)
            {
              if(childNode != null)
              {
                parentNode.Children.Add(childNode);
                var index = _nodes.IndexOf(childNode);
                if(index >= 0)
                {
                  _nodes.RemoveAt(index);
                }
              }
              else 
              {
                childNode = new Node { Name = pair.Child };
                parentNode.Children.Add(childNode);
                _nodeMap.Add(pair.Child, childNode);
              }
            }
            else
            {
                parentNode = new Node { Name = pair.Parent };
                _nodeMap.Add(pair.Parent, parentNode);
                _nodes.Add(parentNode);
                if(childNode == null) 
                {
                  childNode = new Node { Name = pair.Child };
                  parentNode.Children.Add(childNode);
                  _nodeMap.Add(pair.Child, childNode);
                }
                else 
                {
                  parentNode.Children.Add(childNode);
                  var index = _nodes.IndexOf(childNode);
                  if(index >= 0)
                  {
                    _nodes.RemoveAt(index);
                  }
                }
            }
        }
        
        var results = new List<HostClassPairCount>();
        
        foreach(var node in _nodes) 
        {
          processNode(node, results);
        }

        return results;
    }

    private void processNode(Node node, List<HostClassPairCount> counts)
    {
      var result = new HostClassPairCount {Name = node.Name, Count = node.Count() };
       counts.Add(result);
       foreach(var item in node.Children) 
       {
         processNode(item, counts);
       }
    }
    
    private class Node 
    {
        public Node()
        {
            Children = new List<Node>();
            
        }
        public string Name {get;set;}
        public List<Node> Children {get;set;}
        
        public int Count()
        {
            int count = Children.Count;
            foreach(var item in Children)
            {
              count += item.Count();
            }
            return count;
        }
    }
    
    
}

public class HostClassPair
{
    public string Child {get;set; }
    public string Parent { get;set;}
}

public class HostClassPairCount
{
    public string Name {get;set;}
    public int Count {get;set;}
}